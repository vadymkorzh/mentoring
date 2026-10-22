namespace CatalogService.DAL.Services;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> ListAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
