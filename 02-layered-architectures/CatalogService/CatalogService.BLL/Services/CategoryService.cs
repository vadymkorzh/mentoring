using CatalogService.DAL.Models;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;

public class CategoryService(IRepository<Category> repository)
    : CrudService<Category>(repository), ICategoryService
{
}
