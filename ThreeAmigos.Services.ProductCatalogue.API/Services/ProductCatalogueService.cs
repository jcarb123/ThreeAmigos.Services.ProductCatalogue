using AutoMapper;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using ThreeAmigos.Services.ProductCatalogue.API.Models;

namespace ThreeAmigos.Services.ProductCatalogue.API.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
    private readonly IMapper _mapper;

    public ProductCatalogueService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2));
        _mapper = mapper;
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
            throw new Exception("Unable to retrieve products after multiple retries.", e);
        }
    }
}