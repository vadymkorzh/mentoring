using Asp.Versioning;
using CatalogService.BLL.Services;
using CatalogService.DAL.Services;
using CatalogService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
})
.AddOpenApi();

builder.Services.AddMessageBus(builder.Configuration);

builder.Services.AddEfCoreRepositories(builder.Configuration.GetConnectionString("Default")!);
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapOpenApi().WithDocumentPerVersion();

app.Run();

public partial class Program;