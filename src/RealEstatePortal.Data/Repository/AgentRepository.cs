namespace RealEstatePortal.Data.Repository;

using Contracts;
using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Data.Models;

public class AgentRepository : BaseRepository, IAgentRepository
{
    public AgentRepository(RealEstateDbContext dbContext)
        : base(dbContext)
    {

    }

    public async Task<bool> ExistsByIdAsync(string userId)
    {
        return await AllReadonly<Agent>()
            .AnyAsync(a => a.UserId == userId == a.IsDeleted == false);
    }

    public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
    {
        return await AllReadonly<Agent>()
            .AnyAsync(a => a.PhoneNumber == phoneNumber);
    }
}
