namespace CartService.Web.Dtos;

public record CartDto
{
    public required string Key { get; init; }
    public required IReadOnlyList<CartItemDto> Items { get; init; }
}
