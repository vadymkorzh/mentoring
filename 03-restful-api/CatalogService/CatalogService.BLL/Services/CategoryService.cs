using CatalogService.BLL.Exceptions;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;


public class CategoryService(IRepository<Category> categories, ProductService productService)
{
    private readonly IRepository<Category> _categories = categories;
    private readonly ProductService _productService = productService;

    public Task<IReadOnlyList<Category>> ListAsync() => _categories.ListAsync();

    public Task<Category> AddAsync(Category category) => _categories.AddAsync(category);

    public Task UpdateAsync(Category category) => _categories.UpdateAsync(category);

    public async Task DeleteAsync(int id)
    {
        var category = await _categories.GetAsync(id) ?? throw new NotFoundException(typeof(Category).Name, id);
        
        foreach (var product in category.Products)
        {
            await _productService.DeleteAsync(product.Id);
        }

        await _categories.DeleteAsync(id);
    }
}
