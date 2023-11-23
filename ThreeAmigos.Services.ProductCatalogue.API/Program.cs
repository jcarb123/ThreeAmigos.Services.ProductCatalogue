using System.Reflection;
using ThreeAmigos.Services.ProductCatalogue.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCors();

    builder.Services.AddScoped<IProductCatalogueService, ProductCatalogueService>();

builder.Services.AddHttpClient<IProductCatalogueService, ProductCatalogueService>(client =>
{
    client.BaseAddress = new Uri("http://undercutters.azurewebsites.net");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();