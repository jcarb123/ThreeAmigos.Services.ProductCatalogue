using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;

namespace ThreeAmigos.Services.ProductCatalogue.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v1.0/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMediator _mediator;

    public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<GetProducts.Result>> GetProducts([FromQuery] GetProducts.Query query)
    {
        _logger.LogInformation("Getting all products...");

        var result = await _mediator.Send(query);

        if (!result.Products.Any()) return NotFound();

        return Ok(result);
    }
}