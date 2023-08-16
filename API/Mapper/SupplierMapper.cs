using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class SupplierMapper : Profile
    {
        public SupplierMapper() 
        {
            CreateMap<Supplier, SupplierDTO>()
                .ForMember(des => des.SuppliersId,
                            act => act.MapFrom(src => src.SuppliersId))
                .ForMember(des => des.SuppliersName,
                            act => act.MapFrom(src => src.SuppliersName))
                .ForMember(des => des.Address,
                            act => act.MapFrom(src => src.Address))
                .ForMember(des => des.Phone,
                            act => act.MapFrom(src => src.Phone))
                .ForMember(des => des.Email,
                            act => act.MapFrom(src => src.Email));
        }
    }
}
