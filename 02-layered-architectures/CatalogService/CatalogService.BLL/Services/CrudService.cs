using CatalogService.BLL.Exceptions;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;

public class CrudService<T>(IRepository<T> repository) : ICrudService<T> where T : class
{
    public virtual async Task<T> GetAsync(int id)
        => await repository.GetAsync(id) ?? throw new NotFoundException(typeof(T).Name, id);

    public virtual Task<IReadOnlyList<T>> ListAsync() => repository.ListAsync();

    public virtual Task<T> AddAsync(T entity) => repository.AddAsync(entity);

    public virtual Task UpdateAsync(T entity) => repository.UpdateAsync(entity);

    public virtual async Task DeleteAsync(int id)
    {
        await GetAsync(id);
        await repository.DeleteAsync(id);
    }
}
