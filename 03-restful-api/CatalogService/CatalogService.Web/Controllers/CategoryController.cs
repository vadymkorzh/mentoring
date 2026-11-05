using Asp.Versioning;
using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Web.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]

public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> List()
    {
        var categories = await _categoryService.ListAsync();

        return Ok(categories.Select(c => c.ToDto()));
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Add(CategoryRequest request)
    {
        var category = await _categoryService.AddAsync(request.ToEntity());
        var dto = category.ToDto();
        dto.Links =
        [
            new LinkDto { Rel = "update", Href = $"/categories/{dto.Id}", Method = "PUT" },
            new LinkDto { Rel = "delete", Href = $"/categories/{dto.Id}", Method = "DELETE" },
            new LinkDto { Rel = "collection", Href = "/categories", Method = "GET" }
        ];

        return Created($"/categories/{dto.Id}", dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryRequest request)
    {
        await _categoryService.UpdateAsync(request.ToEntity(id));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);

            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
