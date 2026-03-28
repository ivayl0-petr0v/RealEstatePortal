namespace RealEstatePortal.Data.Repository;

using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Data.Repository.Contracts;


public class BaseRepository : IBaseRepository, IDisposable
{
    private readonly RealEstateDbContext dbContext;

    private bool isDisposed = false;

    public BaseRepository(RealEstateDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected DbSet<T> DbSet<T>() where T : class
    {
        return dbContext.Set<T>();
    }

    public IQueryable<T> All<T>() where T : class
    {
        return DbSet<T>();
    }

    public IQueryable<T> AllReadonly<T>() where T : class
    {
        return DbSet<T>().AsNoTracking();
    }

    public async Task<T?> GetByIdAsync<T>(object id) where T : class
    {
        return await DbSet<T>().FindAsync(id);
    }

    public async Task<bool> AddAsync<T>(T entity) where T : class
    {
        await DbSet<T>().AddAsync(entity);
        int result = await SaveChangesAsync();
        return result == 1;
    }

    public void Update<T>(T entity) where T : class
    {
        DbSet<T>().Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        DbSet<T>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        isDisposed = true;
    }
}