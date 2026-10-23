namespace CatalogService.BLL.Exceptions;

public class NotFoundException(string entityName, int id)
    : Exception($"{entityName} with id {id} was not found.");
