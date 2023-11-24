using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Services;

public interface IProductCatalogueService
{
    public Task<IEnumerable<Product>> GetProducts();
    public Task<IEnumerable<Product>> SearchProducts(string searchTerm);
}