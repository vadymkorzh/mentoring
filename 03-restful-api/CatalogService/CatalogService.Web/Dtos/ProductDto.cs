namespace CatalogService.Web.Dtos;

public class ProductDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? DescriptionHtml { get; set; }

    public string? ImageUri { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }
}
