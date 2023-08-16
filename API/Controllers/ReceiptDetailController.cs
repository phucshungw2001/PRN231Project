using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptDetailController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public ReceiptDetailController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllReceiptDetail")]
        public IActionResult Get()
        {
            List<ReceiptDetail> receiptDetails = _context.ReceiptDetails.ToList();
            return Ok(_mapper.Map<List<ReceiptDetailDTO>>(receiptDetails));
        }

        [HttpGet("GetlReceiptDetailById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<ReceiptDetail> receiptDetails = _context.ReceiptDetails.Where(s => s.ReceiptDetailId == id).ToList();
            return Ok(_mapper.Map<List<ReceiptDetailDTO>>(receiptDetails));
        }

        [HttpPost("AddReceiptDetail")]
        public IActionResult Add(ReceiptDetail receiptDetails)
        {
            _context.ReceiptDetails.Add(receiptDetails);
            _context.SaveChanges();
            /* return Ok(_mapper.Map<List<ProductDTO>>(product));*/
            return Ok(receiptDetails);
        }

        [HttpPut("UpdateStockReceipt")]
        public IActionResult Update(ReceiptDetail receiptDetail, int id)
        {
            var receiptDetails = _context.ReceiptDetails.FirstOrDefault(p => p.ReceiptDetailId == id);
            if (receiptDetails == null)
            {
                return NotFound();
            }

            receiptDetails.ReceiptId = receiptDetail.ReceiptId;
            receiptDetails.ProductId = receiptDetail.ProductId;
            receiptDetails.EntryPrice = receiptDetail.EntryPrice;
            receiptDetails.TotalValue = receiptDetail.TotalValue;
            receiptDetails.EntryUnit = receiptDetail.EntryUnit;

            _context.Entry(receiptDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(receiptDetails);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ReceiptDetail receiptDetails = _context.ReceiptDetails.FirstOrDefault(p => p.ReceiptDetailId == id);
            if (receiptDetails == null)
            {
                return NotFound(id);
            }

            _context.ReceiptDetails.Remove(receiptDetails);
            _context.SaveChanges();
            return Ok(receiptDetails);
        }
    }
}
