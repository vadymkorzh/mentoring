namespace CatalogService.Web.Dtos;

public class CategoryDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int? ParentId { get; set; }

    public string? ImageUri { get; set; }
}
