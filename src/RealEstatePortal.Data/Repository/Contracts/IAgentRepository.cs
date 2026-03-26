namespace RealEstatePortal.Data.Repository.Contracts
{
    public interface IAgentRepository : IBaseRepository
    {
        Task<bool> ExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);
    }
}
