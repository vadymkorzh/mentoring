using CatalogService.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure.EfCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfCoreRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        
        return services;
    }
}
