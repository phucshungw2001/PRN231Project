using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class CategoriesMapping : Profile
    {
        public CategoriesMapping() {
            CreateMap<Category, CategoriesDTO>()
                              .ForMember(des => des.CategoryId,
                            act => act.MapFrom(src => src.CategoryId))
                                .ForMember(des => des.CategoryName,
                            act => act.MapFrom(src => src.CategoryName))
                ;
        }
    }
}
