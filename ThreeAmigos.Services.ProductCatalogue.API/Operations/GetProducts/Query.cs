using MediatR;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.GetProducts;

public static partial class GetProducts
{
    public class Query : IRequest<Result>
    {
    }
}