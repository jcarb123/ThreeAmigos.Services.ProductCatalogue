using AutoMapper;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductCatalogueService> _logger;
    private readonly IMapper _mapper;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public ProductCatalogueService(HttpClient httpClient, IMapper mapper, ILogger<ProductCatalogueService> logger)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger ?? throw new ArgumentException("Logger not initialised", nameof(logger));
        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2), (outcome, timespan, retryAttempt, context) =>
            {
                _logger.LogWarning(
                    $"Retry No - {retryAttempt}");
            });
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        try
        {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync("/api/Product"));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var productDtos = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);

            return _mapper.Map<IEnumerable<Product>>(productDtos);
        }
        catch (HttpRequestException e)
        {
            _logger.LogError($"Failed to retrieve products:");
            throw new Exception("Unable to retrieve products after multiple retries.", e);
        }
    }
}