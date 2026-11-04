using CatalogService.BLL.Services;
using CatalogService.DAL.Models;
using CatalogService.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogService.Tests.IntegrationTests;

public class CategoryServiceIntegrationTests : IAsyncLifetime
{
    private readonly string _connectionString =
        $"Server=(localdb)\\MSSQLLocalDB;Database=CatalogServiceTests_{Guid.NewGuid():N};Trusted_Connection=True;TrustServerCertificate=True";

    private CatalogDbContext CreateContext() => new(
        new DbContextOptionsBuilder<CatalogDbContext>().UseSqlServer(_connectionString).Options);

    public async ValueTask InitializeAsync()
    {
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await using var context = CreateContext();
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task AddAsync_ThenGetAsync_PersistsCategoryInDatabase()
    {
        await using var writeContext = CreateContext();
        var writeService = new CategoryService(new EfRepository<Category>(writeContext));

        var added = await writeService.AddAsync(new Category { Name = "Laptops" });

        await using var readContext = CreateContext();
        var readService = new CategoryService(new EfRepository<Category>(readContext));

        var loaded = await readService.GetAsync(added.Id);

        Assert.True(added.Id > 0);
        Assert.Equal("Laptops", loaded.Name);
    }
}
