using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(des => des.UserName,
                            act => act.MapFrom(src => src.UserName))
                .ForMember(des => des.AccountId,
                            act => act.MapFrom(src => src.AccountId))
                .ForMember(des => des.Password,
                            act => act.MapFrom(src => src.Password))
                .ForMember(des => des.IsActive,
                            act => act.MapFrom(src => src.IsActive))
                .ForMember(des => des.Role,
                            act => act.MapFrom(src => src.Role));

            CreateMap<Account, AccountInfo>()
                .ForMember(des => des.UserName,
                            act => act.MapFrom(src => src.UserName))
                .ForMember(des => des.AccountId,
                            act => act.MapFrom(src => src.AccountId))
                .ForMember(des => des.Password,
                            act => act.MapFrom(src => src.Password))
                .ForMember(des => des.IsActive,
                            act => act.MapFrom(src => src.IsActive))
                .ForMember(des => des.Role,
                            act => act.MapFrom(src => src.Role))
                 .ForMember(des => des.Customer,
                            act => act.MapFrom(src => src.Customer))
                 .ForMember(des => des.Manager,
                            act => act.MapFrom(src => src.Manager));
        }
    }
}
