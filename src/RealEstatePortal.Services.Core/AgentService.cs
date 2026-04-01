namespace RealEstatePortal.Services.Core;

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

    public async Task<IEnumerable<AllAgentsViewModel>> GetAllAgentsAsync()
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

    public async Task<AgentDetailsViewModel?> GetAgentDetailsByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out Guid agentId))
        {
            return null;
        }

        var agentDetails = await agentRepository
            .AllReadonly<Agent>()
            .Where(a => a.Id == agentId)
            .Select(a => new AgentDetailsViewModel
            {
                Id = a.Id.ToString(),
                FullName = a.FullName,
                AvatarUrl = a.AvatarUrl ?? "/images/default-avatar.png",
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
                Email = a.User.Email,
                Description = a.Description,
                WorkingDays = a.WorkingDays,
                RestDays = a.RestDays,
                WorkingHours = (a.WorkingHoursStart.HasValue && a.WorkingHoursEnd.HasValue)
                ? $"{a.WorkingHoursStart.Value:hh\\:mm} - {a.WorkingHoursEnd.Value:hh\\:mm}"
                : "Not specified",
                FacebookUrl = a.FacebookUrl,
                WebsiteUrl = a.WebsiteUrl,
                InstagramUrl = a.InstagramUrl,
                SpokenLanguages = a.SpokenLanguages
                .Select(sl => sl.Language.Name)
                .ToArray()
            })
            .FirstOrDefaultAsync();

        return agentDetails;
    }

    public async Task<AgentFormModel?> GetAgentForEditAsync(string id)
    {
        var agent = await baseRepository
            .AllReadonly<Agent>()
            .Include(a => a.SpokenLanguages)
            .ThenInclude(sl => sl.Language)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);

        if (agent == null)
            return null;

        return new AgentFormModel
        {
            FullName = agent.FullName,
            Address = agent.Address,
            PhoneNumber = agent.PhoneNumber,
            Description = agent.Description,
            AvatarUrl = agent.AvatarUrl,
            FacebookUrl = agent.FacebookUrl,
            WebsiteUrl = agent.WebsiteUrl,
            InstagramUrl = agent.InstagramUrl,
            WorkingHoursStart = agent.WorkingHoursStart,
            WorkingHoursEnd = agent.WorkingHoursEnd,
            RestDays = agent.RestDays,
            WorkingDaysStart = agent.WorkingDays?.Split(" - ").FirstOrDefault(),
            WorkingDaysEnd = agent.WorkingDays?.Split(" - ").Skip(1).FirstOrDefault(),
            SpokenLanguages = string.Join(", ", agent.SpokenLanguages.Select(sl => sl.Language.Name))
        };
    }

    public async Task EditAgentAsync(string id, AgentFormModel model)
    {
        var agent = await baseRepository
            .All<Agent>()
            .Include(a => a.SpokenLanguages)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);

        if (agent != null)
        {
            agent.FullName = model.FullName;
            agent.PhoneNumber = model.PhoneNumber;
            agent.Address = model.Address;
            agent.Description = model.Description;
            agent.AvatarUrl = model.AvatarUrl ?? "/images/default-avatar.png";
            agent.FacebookUrl = model.FacebookUrl;
            agent.WebsiteUrl = model.WebsiteUrl;
            agent.InstagramUrl = model.InstagramUrl;
            agent.WorkingHoursStart = model.WorkingHoursStart;
            agent.WorkingHoursEnd = model.WorkingHoursEnd;
            agent.WorkingDays = $"{model.WorkingDaysStart} - {model.WorkingDaysEnd}";
            agent.RestDays = model.RestDays;
            agent.SpokenLanguages.Clear();

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

            await baseRepository.SaveChangesAsync();
        }
    }
}
