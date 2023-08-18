using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        private WarehousesContext _context;
        private IMapper _mapper;

        public InvoicesController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<InvoiceDTO>> GetInvoice()
        {
            var invoices = _context.Invoices
                .Include(o => o.InvoiceDetails)
                .ThenInclude(od => od.Product)
                .ToList();

            return _mapper.Map<List<InvoiceDTO>>(invoices);
        }

        [HttpGet("{InvoiceCustomerId}")]
        public async Task<List<InvoiceDTO>> GetInvoiceForLoggedInCustomer(int InvoiceCustomerId)
        {
            var orders = _context.Invoices
                .Include(o => o.InvoiceDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.CustomerId == InvoiceCustomerId)
                .ToList();

            return _mapper.Map<List<InvoiceDTO>>(orders);
        }



        [HttpPut("{ChangeStatusInvoiceId}")]
        public async Task<IActionResult> UpdateOrderStatus(int ChangeStatusInvoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(ChangeStatusInvoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.InvoicesStatus = !invoice.InvoicesStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return Ok(_mapper.Map<InvoiceDTO>(invoice));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceDTO invoiceDTO)
        {
            if (invoiceDTO == null)
            {
                return BadRequest("Invoice data is missing.");
            }

            var newInvoice = new Invoice
            {
                InvoicesId = invoiceDTO.InvoicesId,
                InvoicesDate = invoiceDTO.InvoicesDate,
                CustomerId = invoiceDTO.CustomerId,
                InvoicesStatus = invoiceDTO.InvoicesStatus
            };

            _context.Invoices.Add(newInvoice);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<InvoiceDTO>(newInvoice));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
    }
}
