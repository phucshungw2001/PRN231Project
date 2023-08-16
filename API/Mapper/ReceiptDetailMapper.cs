using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class ReceiptDetailMapper : Profile
    {
        public ReceiptDetailMapper() 
        {
            CreateMap<ReceiptDetail, ReceiptDetailDTO>()
                    .ForMember(des => des.ReceiptDetailId,
                                act => act.MapFrom(src => src.ReceiptDetailId))
                    .ForMember(des => des.ReceiptId,
                                act => act.MapFrom(src => src.ReceiptId))
                    .ForMember(des => des.ProductId,
                                act => act.MapFrom(src => src.ProductId))
                    .ForMember(des => des.EntryPrice,
                                act => act.MapFrom(src => src.EntryPrice))
                    .ForMember(des => des.TotalValue,
                                act => act.MapFrom(src => src.TotalValue))
                    .ForMember(des => des.EntryUnit,
                                act => act.MapFrom(src => src.EntryUnit));
        }
    }
}
