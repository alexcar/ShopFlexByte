using AutoMapper;

namespace ShopFlexByte.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Domain.Entities.OrderItem, Domain.Entities.ShoppingCartItem>().ReverseMap();
    }
}
