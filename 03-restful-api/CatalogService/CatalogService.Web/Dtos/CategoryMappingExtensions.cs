using CatalogService.DAL.Models;

namespace CatalogService.Web.Dtos;

public static class CategoryMappingExtensions
{
    public static CategoryDto ToDto(this Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        ParentId = category.ParentId,
        ImageUri = category.ImageUri
    };

    public static Category ToEntity(this CategoryRequest request, int id = 0) => new()
    {
        Id = id,
        Name = request.Name,
        ParentId = request.ParentId,
        ImageUri = request.ImageUri
    };
}
