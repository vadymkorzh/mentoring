using System.ComponentModel.DataAnnotations;

namespace CatalogService.DAL.Models;

public class Category
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    public int? ParentId { get; set; }

    public string? ImageUri { get; set; }

    public Category? Parent { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
