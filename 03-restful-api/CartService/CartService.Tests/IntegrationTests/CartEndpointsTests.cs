using System.Net;
using System.Net.Http.Json;
using CartService.Web.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CartService.Tests.IntegrationTests;

public class CartEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CartEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddItemToCart_ThenGetCart_ReturnsAddedItem()
    {
        string cartKey = Guid.NewGuid().ToString();
        var item = new CartItemDto { Id = 1, Name = "Widget", Price = 9.99m, Quantity = 2 };

        var postResponse = await _client.PostAsJsonAsync($"/api/v1/carts/{cartKey}/items", item);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/v1/carts/{cartKey}");
        getResponse.EnsureSuccessStatusCode();

        var cart = await getResponse.Content.ReadFromJsonAsync<CartDto>();
        Assert.NotNull(cart);
        Assert.Equal(cartKey, cart!.Key);
        Assert.Contains(cart.Items, i => i.Id == item.Id && i.Name == item.Name && i.Quantity == item.Quantity);
    }
}
