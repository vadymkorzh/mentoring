using CatalogService.BLL.Exceptions;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;

public class ProductService(IRepository<Product> products)
{
    private readonly IRepository<Product> _products = products;

    public Task<IReadOnlyList<Product>> ListAsync() => _products.ListAsync();

    public Task<Product> AddAsync(Product product) => _products.AddAsync(product);

    public Task UpdateAsync(Product product) => _products.UpdateAsync(product);

    public async Task DeleteAsync(int id)
    {
        _ = await _products.GetAsync(id) ?? throw new NotFoundException(typeof(Product).Name, id);
        
        await _products.DeleteAsync(id);
    }
}
