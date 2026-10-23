using CatalogService.DAL.Models;
using CatalogService.DAL.Services;

namespace CatalogService.BLL.Services;

public class ProductService(IRepository<Product> repository)
    : CrudService<Product>(repository), IProductService
{
}
