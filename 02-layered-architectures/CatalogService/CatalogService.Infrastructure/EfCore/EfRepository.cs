using CatalogService.DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.EfCore;

public class EfRepository<T>(CatalogDbContext context) : IRepository<T> where T : class
{
    public async Task<T?> GetAsync(int id) => await context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> ListAsync() 
        => await context.Set<T>().AsNoTracking().ToListAsync();

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
