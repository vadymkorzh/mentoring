using CatalogService.BLL.Services;
using CatalogService.DAL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();