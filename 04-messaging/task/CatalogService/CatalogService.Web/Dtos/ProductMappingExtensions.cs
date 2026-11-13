using CatalogService.DAL.Models;

namespace CatalogService.Web.Dtos;

public static class ProductMappingExtensions
{
    public static ProductDto ToDto(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        DescriptionHtml = product.DescriptionHtml,
        ImageUri = product.ImageUri,
        CategoryId = product.CategoryId,
        Price = product.Price,
        Amount = product.Amount
    };

    public static Product ToEntity(this ProductRequest request, int id = 0) => new()
    {
        Id = id,
        Name = request.Name,
        DescriptionHtml = request.DescriptionHtml,
        ImageUri = request.ImageUri,
        CategoryId = request.CategoryId,
        Price = request.Price,
        Amount = request.Amount
    };
}
