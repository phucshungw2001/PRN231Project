using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class StockReceiptMapper : Profile
    {
        public StockReceiptMapper() 
        {
            CreateMap<StockReceipt, StockReceiptDTO>()
                        .ForMember(des => des.ReceiptId,
                                    act => act.MapFrom(src => src.ReceiptId))
                        .ForMember(des => des.DateReceipt,
                                    act => act.MapFrom(src => src.DateReceipt))
                        .ForMember(des => des.WarehouseId,
                                    act => act.MapFrom(src => src.WarehouseId))
                        .ForMember(des => des.SupplierId,
                                    act => act.MapFrom(src => src.SupplierId)); 
        }
    }
}
