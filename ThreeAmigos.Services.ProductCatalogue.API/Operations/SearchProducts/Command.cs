using MediatR;

namespace ThreeAmigos.Services.ProductCatalogue.API.Operations.SearchProducts;

public static partial class SearchProducts
{
    public class Command : IRequest<Result>
    {
        public string SearchTerm { get; set; } = "";
    }
}