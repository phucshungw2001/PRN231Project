using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class InvoiceMapping : Profile
    {
        public InvoiceMapping()
        {
            CreateMap<Invoice, InvoiceDTO>()
                 .ForMember(dest => dest.InvoiceDetais, opt => opt.MapFrom(src => src.InvoiceDetails))
                 .ReverseMap();

            CreateMap<InvoiceDetail, InvoiceDetailDTO>().ReverseMap();

        }
    }
}
