using AutoMapper;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly ILogger<ProductCatalogueService> _logger;
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Product> _products;
    private readonly AsyncRetryPolicy _retryPolicy;
    private bool _hasRetried;

    public ProductCatalogueService(IMongoCollection<Product> products, IMapper mapper,
        ILogger<ProductCatalogueService> logger)
    {
        _products = products;
        _mapper = mapper;
        _logger = logger ?? throw new ArgumentException("Logger not initialised", nameof(logger));
        _retryPolicy = Policy
            .Handle<MongoException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2),
                (exception, timespan, retryAttempt, context) =>
                {
                    _hasRetried = true;
                    _logger.LogWarning(
                        $"GetProducts - Attempt {retryAttempt} failed with {exception.Message}. Waiting {timespan} before next retry.");
                });
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        _hasRetried = false;

        try
        {
            var products = await _retryPolicy
                .ExecuteAsync(async context => { return await _products.Find(p => p.InStock).ToListAsync(); },
                    new Context("GetProducts"))
                .ContinueWith(task =>
                {
                    if (_hasRetried && task.Status == TaskStatus.RanToCompletion)
                        _logger.LogInformation("GetProducts operation succeeded after one or more retries.");

                    return task;
                }).Unwrap();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }
        catch (MongoException e)
        {
            _logger.LogError($"Failed to retrieve products in GetProducts: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> SearchProducts(string searchTerm)
    {
        var products = await GetProducts();
        var productList = products.ToList();

        if (string.IsNullOrWhiteSpace(searchTerm)) return productList;

        var filteredProducts = productList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();

        return filteredProducts.Any() ? filteredProducts : productList;
    }
}