using MediatR;
using ThreeAmigos.Services.ProductCatalogue.API.Services;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;

public static partial class GetProducts
{
    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly IProductCatalogueService _productService;
        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger, IProductCatalogueService productService)
        {
            _logger = logger ?? throw new ArgumentException("Logger not initialised", nameof(logger));
            _productService = productService ??
                              throw new ArgumentException("Service not initialised",
                                  nameof(productService));
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all products....");

            var products = await _productService.GetProducts();

            _logger.LogInformation($"Successfully retrieved. Total products - {products.Count()}");
            return new Result
            {
                Products = products
            };
        }
    }
}