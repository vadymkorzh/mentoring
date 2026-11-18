using CatalogService.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogService.Tests.IntegrationTests;

public class CategoryServiceIntegrationTests
{
    private readonly string _connectionString =
        $"Server=(localdb)\\MSSQLLocalDB;Database=CatalogServiceDb;Trusted_Connection=True;TrustServerCertificate=True";

    private CatalogDbContext CreateContext() => new(
        new DbContextOptionsBuilder<CatalogDbContext>().UseSqlServer(_connectionString).Options);

    [Fact]
    public async Task InitializeAsync()
    {
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }
}
