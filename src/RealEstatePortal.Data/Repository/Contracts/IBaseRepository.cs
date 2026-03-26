namespace RealEstatePortal.Data.Repository.Contracts
{
    public interface IBaseRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        Task<T?> GetByIdAsync<T>(object id) where T : class;

        Task<bool> AddAsync<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();
    }
}
