using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CartService.BLL;
using CartService.DAL;
using CartService.Infrastructure;
using Scalar.AspNetCore;

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

builder.Services.AddLogging();

builder.Services.AddScoped<ICartRepository, LiteDbCartRepository>();
builder.Services.AddScoped<CartManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().WithDocumentPerVersion();

    app.MapScalarApiReference(options =>
    {
        var versionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (ApiVersionDescription description in versionProvider.ApiVersionDescriptions)
        {
            options.AddDocument(description.GroupName, $"Cart Service {description.GroupName}", $"/openapi/{description.GroupName}.json");
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program;
