namespace ThreeAmigos.Services.ProductCatalogue.API.Models;

/// <summary>
/// Product DTO model
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = "";
    
    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Product availability status
    /// </summary>
    public bool InStock { get; set; }
    
    /// <summary>
    /// Product calories
    /// </summary>
    public int Calories { get; set; }
    
    /// <summary>
    /// Product image url
    /// </summary>
    public string ImageUrl { get; set; } = "";
}