using AutoMapper;

namespace ThreeAmigos.Services.ProductCatalogue.API.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
            .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Calories))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}