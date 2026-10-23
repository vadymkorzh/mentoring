using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using Moq;
using Xunit;

namespace CatalogService.Tests;

public class ProductServiceTests
{
    private readonly Mock<IRepository<Product>> _repository = new();
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _service = new ProductService(_repository.Object);
    }

    [Fact]
    public async Task DeleteAsync_ExistingId_DeletesFromRepository()
    {
        var product = new Product { Id = 1, Name = "Laptop", Price = 1000, Amount = 2 };
        _repository.Setup(r => r.GetAsync(1)).ReturnsAsync(product);

        await _service.DeleteAsync(1);

        _repository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_MissingId_ThrowsAndDoesNotDelete()
    {
        _repository.Setup(r => r.GetAsync(2)).ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(2));

        _repository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
