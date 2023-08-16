using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private WarehousesContext _context;
        private IMapper _mapper;

        public InvoicesController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{OrderCustomerID}")]
        public async Task<List<InvoiceDTO>> GetInvoiceForLoggedInCustomer(int OrderCustomerID)
        {
            var orders = _context.Invoices
                .Include(o => o.InvoiceDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.CustomerId == OrderCustomerID)
                .ToList();

            return _mapper.Map<List<InvoiceDTO>>(orders);
        }

        [HttpGet("{All}")]
        public async Task<List<InvoiceDTO>> GetInvoice()
        {
            var orders = _context.Invoices
                .Include(o => o.InvoiceDetails)
                .ThenInclude(od => od.Product)
                .ToList();

            return _mapper.Map<List<InvoiceDTO>>(orders);
        }
    }
}
