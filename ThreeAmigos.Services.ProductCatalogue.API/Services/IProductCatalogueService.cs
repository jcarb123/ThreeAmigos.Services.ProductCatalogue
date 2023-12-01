using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Services;

public interface IProductCatalogueService
{
    public Task<IEnumerable<ProductDto>> GetProducts();
    public Task<IEnumerable<ProductDto>> SearchProducts(string searchTerm);
}