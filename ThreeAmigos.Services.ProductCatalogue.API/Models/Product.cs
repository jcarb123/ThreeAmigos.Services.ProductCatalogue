namespace ThreeAmigos.Services.ProductCatalogue.API.Models;

public class Product
{
    public string CategoryName { get; set; } = "";
    public string BrandName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}