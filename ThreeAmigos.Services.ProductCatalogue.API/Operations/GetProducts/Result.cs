using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;

public static partial class GetProducts
{
    public class Result
    {
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    }
}