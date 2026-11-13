using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using Moq;
using Xunit;

namespace CatalogService.Tests.UnitTests;

public class CategoryServiceTests
{
    [Fact]
    public async Task AddAsync_AddsCategoryToRepository()
    {
        var categories = new Mock<IRepository<Category>>();
        var products = new Mock<IRepository<Product>>();
        var category = new Category { Name = "Books" };

        categories.Setup(x => x.AddAsync(category)).ReturnsAsync(category);

        var service = new CategoryService(categories.Object, products.Object);
        var result = await service.AddAsync(category);

        Assert.Same(category, result);
        categories.Verify(x => x.AddAsync(category), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_CategoryNotFound_ThrowsNotFoundException()
    {
        var categories = new Mock<IRepository<Category>>();
        var products = new Mock<IRepository<Product>>();

        categories.Setup(x => x.GetAsync(1)).ReturnsAsync((Category?)null);

        var service = new CategoryService(categories.Object, products.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(1));
    }
}
