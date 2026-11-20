using System.Net;
using System.Net.Http.Json;
using CatalogService.Web.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CatalogService.Tests.IntegrationTests;

public class ProductEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddProduct_ThenList_ReturnsCreatedProduct()
    {
        var request = new ProductRequest { Name = "Laptop", CategoryId = 1, Price = 999.99m, Amount = 10 };

        var postResponse = await _client.PostAsJsonAsync("/api/v1/products", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(created);

        var listResponse = await _client.GetAsync("/api/v1/products");
        listResponse.EnsureSuccessStatusCode();

        var products = await listResponse.Content.ReadFromJsonAsync<PagedResultDto<ProductDto>>();
        Assert.Contains(products!.Items, p => p.Id == created!.Id && p.Name == "Laptop");
    }
}
