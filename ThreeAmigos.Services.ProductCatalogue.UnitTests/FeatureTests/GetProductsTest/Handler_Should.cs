using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ThreeAmigos.Services.ProductCatalogue.API.Models;
using ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;
using Xunit;
using Moq;
using Shouldly;

namespace ThreeAmigos.Services.ProductCatalogue.UnitTests.FeatureTests.GetProductsTest;

public class HandlerShould : TestBase
{
    private readonly ILogger<GetProducts.Handler> _logger = new NullLogger<GetProducts.Handler>();

    [Fact]
    public async Task Return_Products()
    {
        // Given
        var query = new GetProducts.Query();

        var expectedProducts = new List<Product>
        {
            ProductStub
        };

        MockService.Setup(x => x.GetProducts()).ReturnsAsync(expectedProducts);


        var handler = new GetProducts.Handler(_logger, MockService.Object);

        // When
        var result = await handler.Handle(query, CancellationToken.None);

        // Then
        result.ShouldNotBeNull();
        result.Products.ShouldNotBeNull();
        result.Products.ShouldBe(expectedProducts);
    }
}