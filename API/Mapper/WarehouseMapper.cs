using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class WarehouseMapper : Profile
    {
        public WarehouseMapper()
        {
            CreateMap<Warehouse, WarehouseDTO>()
                .ForMember(des => des.WarehouseId,
                            act => act.MapFrom(src => src.WarehouseId))
                .ForMember(des => des.WarehouseName,
                            act => act.MapFrom(src => src.WarehouseName))
                .ForMember(des => des.Address,
                            act => act.MapFrom(src => src.Address))
                .ForMember(des => des.TotalProducts,
                            act => act.MapFrom(src => src.Products.Sum(product => product.ProductId)))
                  .ForMember(des => des.Products,
                            act => act.MapFrom(src => src.Products));
        }
    }
}
