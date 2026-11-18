using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.EfCore;

public class EfRepository<T>(CatalogDbContext context) : IRepository<T> where T : class, IEntity
{
    public async Task<T?> GetAsync(int id) => await context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> ListAsync()
        => await context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<PagedResult<T>> ListAsync(int page, int pageSize, Func<T, bool>? filter = null)
    {
        int count = await context.Set<T>().CountAsync();
        
        IEnumerable<T> items = context.Set<T>();
        if (filter != null)
        {
            items = items.Where(filter);
        }
        items = items
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return new()
        {
            TotalCount = count,
            Items = (List<T>)items
        };
    }

    public async Task<T> AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await context.Set<T>()
            .Where(e => EF.Property<int>(e, "Id") == id)
            .ExecuteDeleteAsync();
    }
}
