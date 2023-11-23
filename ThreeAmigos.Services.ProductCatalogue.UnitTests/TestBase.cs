using AutoMapper;
using MediatR;
using Moq;
using ThreeAmigos.Services.ProductCatalogue.API.Models;
using ThreeAmigos.Services.ProductCatalogue.API.Services;

namespace ThreeAmigos.Services.ProductCatalogue.UnitTests;

public class TestBase
{
    protected readonly IMapper Mapper;
    protected readonly Mock<IMapper> MockMapper;
    protected readonly Mock<IMediator> MockMediator;
    protected readonly Mock<IProductCatalogueService> MockService;
    protected readonly Product ProductStub;

    protected TestBase()
    {
        MockMediator = new Mock<IMediator>();
        MockMapper = new Mock<IMapper>();
        MockService = new Mock<IProductCatalogueService>();
        Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies())));
        ProductStub = new Product
        {
            CategoryName = "Test Category",
            BrandName = "Test Brand",
            Name = "Test Product",
            Description = "This is a description of the test product.",
            Price = 11.45m,
            InStock = true
        };
    }
}