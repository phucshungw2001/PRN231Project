using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceDetailController : ControllerBase
    {
        private WarehousesContext _context;
        private IMapper _mapper;

        public InvoiceDetailController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<InvoiceDetailDTO>> GetInvoiceDetail()
        {
            var invoiceDetail = _context.InvoiceDetails
                .Include(od => od.Product)
                .ToList();

            return _mapper.Map<List<InvoiceDetailDTO>>(invoiceDetail);
        }


        [HttpPost("add-invoice-detail")]
        public async Task<IActionResult> AddInvoiceDetailToInvoice(int invoiceId, InvoiceDetailDTO invoiceDetailDTO)
        {

            if (invoiceDetailDTO == null)
            {
                return BadRequest("InvoiceDetail data is missing.");
            }

            var invoice = await _context.Invoices.Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.InvoicesId == invoiceId);
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            var newInvoiceDetail = new InvoiceDetail
            {
                InvoiceId = invoiceId,
                ProductId = invoiceDetailDTO.ProductId,
                Quantity = invoiceDetailDTO.Quantity,
                Price = invoiceDetailDTO.Price,
                Discount = invoiceDetailDTO.Discount
            };

            invoice.InvoiceDetails.Add(newInvoiceDetail);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<InvoiceDetailDTO>(newInvoiceDetail));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
    }
}
