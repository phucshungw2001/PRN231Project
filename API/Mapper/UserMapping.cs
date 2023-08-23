using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Manager, ManagerDTO>();
        }
    }
}
