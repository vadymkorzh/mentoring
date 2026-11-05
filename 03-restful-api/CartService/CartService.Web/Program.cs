using Asp.Versioning;
using CartService.BLL;
using CartService.DAL;
using CartService.Infrastructure;

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

builder.Services.AddScoped<ICartRepository, LiteDbCartRepository>();
builder.Services.AddScoped<CartManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().WithDocumentPerVersion();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
