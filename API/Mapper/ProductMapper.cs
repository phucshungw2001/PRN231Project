using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper() 
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.ProductId,
                            act => act.MapFrom(src => src.ProductId))
                .ForMember(des => des.ProductName,
                            act => act.MapFrom(src => src.ProductName))
                .ForMember(des => des.Describe,
                            act => act.MapFrom(src => src.Describe))
                .ForMember(des => des.Quantity,
                            act => act.MapFrom(src => src.Quantity))
                .ForMember(des => des.Price,
                            act => act.MapFrom(src => src.Price))
                .ForMember(des => des.WarehouseId,
                            act => act.MapFrom(src => src.WarehouseId))
                .ForMember(des => des.CategoryId,
                            act => act.MapFrom(src => src.CategoryId))
                .ForMember(des => des.Status,
                            act => act.MapFrom(src => src.Status))
                .ForMember(des => des.SuppliersId,
                            act => act.MapFrom(src => src.SuppliersId));
        }
    }
}
