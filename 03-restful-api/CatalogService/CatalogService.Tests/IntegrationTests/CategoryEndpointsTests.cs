using System.Net;
using System.Net.Http.Json;
using CatalogService.Web.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CatalogService.Tests.IntegrationTests;

public class CategoryEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoryEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddCategory_ThenList_ReturnsCreatedCategory()
    {
        var request = new CategoryRequest { Name = "Electronics" };

        var postResponse = await _client.PostAsJsonAsync("/categories", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<CategoryDto>();
        Assert.NotNull(created);

        var listResponse = await _client.GetAsync("/categories");
        listResponse.EnsureSuccessStatusCode();

        var categories = await listResponse.Content.ReadFromJsonAsync<List<CategoryDto>>();
        Assert.Contains(categories!, c => c.Id == created!.Id && c.Name == "Electronics");
    }
}
