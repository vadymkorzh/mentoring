using CatalogService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Web;

[ApiController]
[Route("categories")]
public class CategoryController(CategoryService categoryService)
{
    private readonly CategoryService _categoryService = categoryService;
}
