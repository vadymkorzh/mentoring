using CatalogService.DAL.Models;

namespace CatalogService.DAL.Services;

public interface IRepository<T> where T : class, IEntity
{
    Task<T?> GetAsync(int id);
    Task<IReadOnlyList<T>> ListAsync();
    Task<PagedResult<T>> ListAsync(int page, int pageSize, Func<T, bool>? filter = null);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
