namespace RealEstatePortal.Data.Repository
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using RealEstatePortal.Data.Models;

    public class AgentRepository : BaseRepository, IAgentRepository
    {
        private readonly RealEstateDbContext dbContext;

        public AgentRepository(RealEstateDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            return await AllReadonly<Agent>().AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await AllReadonly<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
