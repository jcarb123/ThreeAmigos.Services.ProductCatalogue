using System.Reflection;
using MongoDB.Driver;
using ThreeAmigos.Services.ProductCatalogue.API.Models;
using ThreeAmigos.Services.ProductCatalogue.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.ToString()); });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCors(options => options.AddDefaultPolicy(bld =>
{
    bld
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddScoped<IProductCatalogueService, ProductCatalogueService>();

var connectionString = Environment.GetEnvironmentVariable("COSMOS_DB_CONNECTION_STRING");
var databaseName = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME");
var collectionName = Environment.GetEnvironmentVariable("COSMOS_DB_COLLECTION_NAME");

builder.Services.AddSingleton<IMongoClient, MongoClient>(s => new MongoClient(connectionString));
builder.Services.AddScoped(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase(databaseName);
    return database.GetCollection<Product>(collectionName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();