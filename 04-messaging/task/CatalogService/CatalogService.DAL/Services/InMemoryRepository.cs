using System.Collections.Concurrent;
using CatalogService.DAL.Models;

namespace CatalogService.DAL.Services;

public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly ConcurrentDictionary<int, T> _entities = new();
    private int _nextId;

    public Task<T?> GetAsync(int id) => Task.FromResult(_entities.GetValueOrDefault(id));

    public Task<IReadOnlyList<T>> ListAsync() => Task.FromResult<IReadOnlyList<T>>(_entities.Values.ToList());

    public Task<PagedResult<T>> ListAsync(int page, int pageSize, Func<T, bool>? filter = null)
    {
        var filtered = filter is null ? _entities.Values.ToList() : _entities.Values.Where(filter).ToList();

        var items = filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(new PagedResult<T>
        {
            Items = items,
            TotalCount = filtered.Count,
            Page = page,
            PageSize = pageSize
        });
    }

    public Task<T> AddAsync(T entity)
    {
        entity.Id = Interlocked.Increment(ref _nextId);
        _entities[entity.Id] = entity;
        
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _entities[entity.Id] = entity;
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _entities.TryRemove(id, out _);
        
        return Task.CompletedTask;
    }
}
