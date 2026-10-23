# Testability (show by example that you can cover your code with Unit and Integration tests, no need to cover all the lines)

You can see the test examples (both unit and integration) in the _CatalogService.Tests_ project.

# Extensibility (describe for what points/costs your code can be extended)

Again, like in the simpler case from the Task 1, _IRepository_ interface serves an abstraction which provides an extensibility point
and facilitates the Dependency Inversion Principle. It is quite easy to implement the repository using any different persistence
technology. 

In case of the interfaces introduced to the BLL, they serve the DIP and OCP principles, but the extensibility value is less visible
as there is less pragmatic sense in implementing them differently than using an implementation of the _IRepository_ interface,
utilized in _CrudService_ class.