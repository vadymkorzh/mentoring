using System.ComponentModel.DataAnnotations;

namespace CatalogService.DAL.Models;

public class Product
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    public string? DescriptionHtml { get; set; }

    public string? ImageUri { get; set; }

    public int CategoryId { get; set; }
    
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Amount { get; set; }

    public Category Category { get; set; } = null!;
}
