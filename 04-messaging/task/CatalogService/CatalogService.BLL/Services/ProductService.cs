using CatalogService.BLL.Exceptions;
using CatalogService.DAL.Models;
using CatalogService.DAL.Services;
using MessageBus.Core;
using MessageBus.Core.Messages;

namespace CatalogService.BLL.Services;

public class ProductService(IRepository<Product> products, IMessageBusClient messageBusClient)
{
    private readonly IRepository<Product> _products = products;

    public Task<PagedResult<Product>> ListAsync(int page, int pageSize, int? categoryId) =>
        _products.ListAsync(page, pageSize, categoryId.HasValue ? p => p.CategoryId == categoryId.Value : null);

    public Task<Product> AddAsync(Product product) => _products.AddAsync(product);

    public async Task UpdateAsync(Product product)
    {
        await SendProductUpdateMessage(product);
        await _products.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _products.GetAsync(id) ?? throw new NotFoundException(typeof(Product).Name, id);
        
        await _products.DeleteAsync(id);
    }

    private async Task SendProductUpdateMessage(Product product)
    {
        IMessageBusSender sender = messageBusClient.CreateSender();
        ProductUpdate update = new()
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };
        await sender.SendProductUpdate(update);
    }
}
