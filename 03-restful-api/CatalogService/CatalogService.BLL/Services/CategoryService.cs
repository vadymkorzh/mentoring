using CatalogService.BLL.Exceptions;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;


public class CategoryService(IRepository<Category> categories, IRepository<Product> products)
{
    private readonly IRepository<Category> _categories = categories;
    private readonly IRepository<Product> _products = products;

    public Task<IReadOnlyList<Category>> ListAsync() => _categories.ListAsync();

    public Task<Category> AddAsync(Category category) => _categories.AddAsync(category);

    public Task UpdateAsync(Category category) => _categories.UpdateAsync(category);

    public async Task DeleteAsync(int id)
    {
        var category = await _categories.GetAsync(id) ?? throw new NotFoundException(typeof(Category).Name, id);
        var categoryProducts = (await _products.ListAsync()).Where(x => x.CategoryId == id);

        foreach (var product in categoryProducts)
        {
            await _products.DeleteAsync(product.Id);
        }

        await _categories.DeleteAsync(id);
    }
}
