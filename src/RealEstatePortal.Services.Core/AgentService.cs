namespace RealEstatePortal.Services.Core
{
    using Contracts;
    using Data.Models;
    using Data.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using RealEstatePortal.GCommon.Exceptions;
    using Web.ViewModels.Agent;

    public class AgentService : IAgentService
    {
        private readonly IAgentRepository agentRepository;
        private readonly IBaseRepository baseRepository;

        public AgentService(IAgentRepository agentRepository, IBaseRepository baseRepository)
        {
            this.agentRepository = agentRepository;
            this.baseRepository = baseRepository;
        }

        public Task<bool> ExistsByIdAsync(string userId)
        {
            return agentRepository.ExistsByIdAsync(userId);
        }

        public Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return agentRepository.UserWithPhoneNumberExistsAsync(phoneNumber);
        }

        public async Task<IEnumerable<AllAgentsViewModel>> GetAllAgents()
        {
            var allAgents = await agentRepository
                .AllReadonly<Agent>()
                .Select(a => new AllAgentsViewModel
                {
                    Id = a.Id.ToString(),
                    FullName = a.FullName,
                    Address = a.Address,
                    PhoneNumber = a.PhoneNumber,
                    AvatarUrl = a.AvatarUrl ?? "/images/default-avatar.png"
                })
                .ToArrayAsync();

            return allAgents;
        }

        public async Task CreateAgentAsync(string userId, AgentFormModel model)
        {
            var agent = new Agent
            {
                UserId = userId,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Description = model.Description,
                AvatarUrl = model.AvatarUrl ?? "/images/default-avatar.png",
                FacebookUrl = model.FacebookUrl,
                WebsiteUrl = model.WebsiteUrl,
                InstagramUrl = model.InstagramUrl,
                WorkingHoursStart = model.WorkingHoursStart,
                WorkingHoursEnd = model.WorkingHoursEnd,
                WorkingDays = $"{model.WorkingDaysStart} - {model.WorkingDaysEnd}",
                RestDays = model.RestDays
            };

            if (!string.IsNullOrWhiteSpace(model.SpokenLanguages))
            {
                var languageNames = model.SpokenLanguages
                    .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())
                    .Distinct();

                foreach (var langName in languageNames)
                {
                    var existingLanguage = await baseRepository.All<Language>()
                        .FirstOrDefaultAsync(l => l.Name.ToLower() == langName.ToLower());

                    if (existingLanguage != null)
                    {
                        agent.SpokenLanguages.Add(new AgentLanguage { LanguageId = existingLanguage.Id });
                    }
                    else
                    {
                        agent.SpokenLanguages.Add(new AgentLanguage { Language = new Language { Name = langName } });
                    }
                }
            }

            bool successAdd = await baseRepository.AddAsync(agent);
            if (!successAdd)
            {
                throw new DbEntityCreateFailureException();
            }
        }
    }
}
