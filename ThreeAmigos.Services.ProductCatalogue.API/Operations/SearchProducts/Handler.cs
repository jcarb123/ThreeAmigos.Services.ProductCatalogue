using MediatR;
using ThreeAmigos.Services.ProductCatalogue.API.Services;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.SearchProducts;

public static partial class SearchProducts
{
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IProductCatalogueService _productService;

        public Handler(ILogger<Handler> logger, IProductCatalogueService productService)
        {
            _logger = logger ?? throw new ArgumentException("Logger not initialised", nameof(logger));
            _productService = productService ??
                              throw new ArgumentException("Service not initialised",
                                  nameof(productService));
        }

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving all products that contain {command.SearchTerm}....");

            var products = (await _productService.SearchProducts(command.SearchTerm)).ToList();

            _logger.LogInformation(
                $"Successfully retrieved. Total products with {command.SearchTerm} - {products.Count}");
            return new Result
            {
                Products = products
            };
        }
    }
}