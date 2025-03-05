using AutoMapper;
using drugstore_branch.Domain.Dto;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Utils;

/*
 * The MappingProfile class configures object-to-object mappings between 
 * domain models and DTOs (Data Transfer Objects). This is done using 
 * AutoMapper. It defines mappings for Order, Product, and Batch models, 
 * as well as specific mappings for Create and Update DTOs. The mappings 
 * include custom behavior such as ignoring certain properties and 
 * reverse mappings for bidirectional conversion.
 */
public class MappingProfile : Profile
{
    /* 
     * Constructor that configures the mappings between the domain models and 
     * DTOs.
     */
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.ProductQuantities, opt => opt.MapFrom(src => src.ProductQuantities));

        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.ProductQuantities, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore());

        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Batch, BatchDto>().ReverseMap();

        CreateMap<CreateBatchDto, Batch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}