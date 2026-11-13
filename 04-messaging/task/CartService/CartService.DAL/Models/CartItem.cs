namespace CartService.DAL.Models;

public record struct CartItem
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
    public ProductImage? Image { get; init; }
}

public record struct ProductImage
{
    public required string Url { get; init; }
    public required string AltText { get; init; }
}