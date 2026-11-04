using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Services;
using CatalogService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Web;

[ApiController]
[Route("products")]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ProductDto>>> List(int page = 1, int pageSize = 20, int? categoryId = null)
    {
        var result = await _productService.ListAsync(page, pageSize, categoryId);

        return Ok(result.ToDto(p => p.ToDto()));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Add(ProductRequest request)
    {
        var product = await _productService.AddAsync(request.ToEntity());
        var dto = product.ToDto();

        return Created($"/products/{dto.Id}", dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductRequest request)
    {
        await _productService.UpdateAsync(request.ToEntity(id));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteAsync(id);

            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
