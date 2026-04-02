namespace RealEstatePortal.Services.Core.Contracts;

using System.Threading.Tasks;
using Web.ViewModels.Agent;

public interface IAgentService
{
    Task<bool> ExistsByIdAsync(string userId);

    Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

    Task<IEnumerable<AllAgentsViewModel>> GetAllAgentsAsync();

    Task CreateAgentAsync(string userId, AgentFormModel model);

    Task<AgentDetailsViewModel?> GetAgentDetailsByIdAsync(string id);

    Task<AgentFormModel?> GetAgentForEditAsync(string id);

    Task EditAgentAsync(string id, AgentFormModel model);

    Task<AgentDeleteViewModel?> GetAgentForDeleteByIdAsync(string id);

    Task DeleteAgentAsync(string id);

    Task<string?> GetAgentUserIdAsync(string agentId);
}
