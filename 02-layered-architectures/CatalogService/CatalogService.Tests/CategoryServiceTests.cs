using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using Moq;
using Xunit;

namespace CatalogService.Tests;

public class CategoryServiceTests
{
    private readonly Mock<IRepository<Category>> _repository = new();
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _service = new CategoryService(_repository.Object);
    }

    [Fact]
    public async Task GetAsync_ExistingId_ReturnsCategory()
    {
        var category = new Category { Id = 1, Name = "Laptops" };
        _repository.Setup(r => r.GetAsync(1)).ReturnsAsync(category);

        var result = await _service.GetAsync(1);

        Assert.Same(category, result);
    }

    [Fact]
    public async Task GetAsync_MissingId_ThrowsNotFoundException()
    {
        _repository.Setup(r => r.GetAsync(2)).ReturnsAsync((Category?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetAsync(2));
    }
}
