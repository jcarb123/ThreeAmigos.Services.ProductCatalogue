using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThreeAmigos.Services.ProductCatalogue.API.Models;

/// <summary>
/// Product model
/// </summary>
public class Product
{
    /// <summary>
    /// Document ID of article
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public string? Id { get; set; }

    /// <summary>
    /// Product code
    /// </summary>
    public string ProductCode { get; set; } = "";
    
    /// <summary>
    /// Category name of product
    /// </summary>
    public string CategoryName { get; set; } = "";
    
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