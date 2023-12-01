using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.SearchProducts;

public static partial class SearchProducts
{
    public class Result
    {
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    }
}