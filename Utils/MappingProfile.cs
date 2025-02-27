using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderDate,
                opt => opt.MapFrom(src => src.OrderDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.ProductIds,
                opt => opt.MapFrom(src => src.Products.Select(p => p.Id).ToList()));

        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
}
