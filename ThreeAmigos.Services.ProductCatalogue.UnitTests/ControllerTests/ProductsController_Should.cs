using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using ThreeAmigos.Services.ProductCatalogue.API.Controllers;
using ThreeAmigos.Services.ProductCatalogue.API.Models;
using ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;
using Xunit;

namespace ThreeAmigos.Services.ProductCatalogue.UnitTests.ControllerTests;

public class ProductsControllerShould : TestBase
{
    private readonly ILogger<ProductsController> _logger = new NullLogger<ProductsController>();

    [Fact]
    public async Task GetProducts_WhenCalled()
    {
        // Given
        var query = new GetProducts.Query();
        var mockResult = new GetProducts.Result
        {
            Products = new List<Product>()
            {
                ProductStub
            }
        };

        MockMediator.Setup(m => m.Send(It.IsAny<GetProducts.Query>(), CancellationToken.None))
            .ReturnsAsync(mockResult);

        var controller = new ProductsController(_logger, MockMediator.Object);

        // When
        var result = await controller.GetProducts(query);

        // Then
        result.ShouldNotBeNull();
        result.Result.ShouldBeOfType<OkObjectResult>();
        var objectResult = result.Result as OkObjectResult;
        objectResult.ShouldNotBeNull();
        var actualResult = objectResult.Value as GetProducts.Result;
        actualResult.ShouldNotBeNull();
        actualResult.ShouldBeEquivalentTo(mockResult);
    }

    [Fact]
    public async Task ReturnNotFound_WhenGetProductsIsCalled_AndIsNull()
    {
        // Given
        var query = new GetProducts.Query();
        MockMediator.Setup(m => m.Send(It.IsAny<GetProducts.Query>(), CancellationToken.None))
            .ReturnsAsync(new GetProducts.Result());

        var controller = new ProductsController(_logger, MockMediator.Object);

        // When
        var result = await controller.GetProducts(query);

        // Then
        result.ShouldNotBeNull();
        result.Result.ShouldBeOfType<NotFoundResult>();
        var notFoundResult = result.Result as NotFoundResult;
        notFoundResult.ShouldNotBeNull();
    }
}