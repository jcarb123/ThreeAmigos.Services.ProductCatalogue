using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using ThreeAmigos.Services.ProductCatalogue.API.Models;
using ThreeAmigos.Services.ProductCatalogue.API.Operations.SearchProducts;
using Xunit;

namespace ThreeAmigos.Services.ProductCatalogue.UnitTests.FeatureTests.SearchProductsTest;

public class HandlerShould : TestBase
{
    private readonly ILogger<SearchProducts.Handler> _logger = new NullLogger<SearchProducts.Handler>();

    [Fact]
    public async Task Return_Products_From_Search()
    {
        // Given
        var command = new SearchProducts.Command { SearchTerm = "te" };

        var expectedProducts = new List<ProductDto>
        {
            ProductStub
        };

        MockService.Setup(x => x.SearchProducts(command.SearchTerm)).ReturnsAsync(expectedProducts);


        var handler = new SearchProducts.Handler(_logger, MockService.Object);

        // When
        var result = await handler.Handle(command, CancellationToken.None);

        // Then
        result.ShouldNotBeNull();
        result.Products.ShouldNotBeNull();
        result.Products.ShouldBe(expectedProducts);
    }
}