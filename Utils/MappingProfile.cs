using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.ProductQuantities,
                opt => opt.MapFrom(src => src.ProductQuantities));
            
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.ProductQuantities, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore());
    }
}
