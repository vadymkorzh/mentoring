using CartService.DAL.Models;

namespace CartService.Web.Dtos;

public static class CartMappingExtensions
{
    public static CartItemDto ToDto(this CartItem item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        Quantity = item.Quantity,
        Image = item.Image?.ToDto()
    };

    public static ProductImageDto ToDto(this ProductImage image) => new()
    {
        Url = image.Url,
        AltText = image.AltText
    };

    public static CartItem ToModel(this CartItemDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Price = dto.Price,
        Quantity = dto.Quantity,
        Image = dto.Image?.ToModel()
    };

    public static ProductImage ToModel(this ProductImageDto dto) => new()
    {
        Url = dto.Url,
        AltText = dto.AltText
    };
}
