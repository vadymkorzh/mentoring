using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using MessageBus.Core;
using Moq;
using Xunit;

namespace CatalogService.Tests.UnitTests;

public class ProductServiceTests
{
    [Fact]
    public async Task AddAsync_AddsProductToRepository()
    {
        var products = new Mock<IRepository<Product>>();
        var messageBusClient = new Mock<IMessageBusClient>();
        var product = new Product { Name = "Book", CategoryId = 1 };

        products.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

        var service = new ProductService(products.Object, messageBusClient.Object);
        var result = await service.AddAsync(product);

        Assert.Same(product, result);
        products.Verify(x => x.AddAsync(product), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ProductNotFound_ThrowsNotFoundException()
    {
        var products = new Mock<IRepository<Product>>();
        var messageBusClient = new Mock<IMessageBusClient>();

        products.Setup(x => x.GetAsync(1)).ReturnsAsync((Product?)null);

        var service = new ProductService(products.Object, messageBusClient.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(1));
    }
}
