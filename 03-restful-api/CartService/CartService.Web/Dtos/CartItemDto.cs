namespace CartService.Web.Dtos;

public record ProductImageDto
{
    public required string Url { get; init; }
    public required string AltText { get; init; }
}

public record CartItemDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
    public ProductImageDto? Image { get; init; }
}
