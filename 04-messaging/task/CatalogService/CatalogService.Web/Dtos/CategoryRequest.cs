using System.ComponentModel.DataAnnotations;

namespace CatalogService.Web.Dtos;

public class CategoryRequest
{
    [MaxLength(50)]
    public required string Name { get; set; }

    public int? ParentId { get; set; }

    public string? ImageUri { get; set; }
}
