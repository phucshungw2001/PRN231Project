using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockReceiptController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public StockReceiptController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllStockReceipt")]
        public IActionResult Get()
        {
            List<StockReceipt> stockReceipts = _context.StockReceipts.ToList();
            return Ok(_mapper.Map<List<StockReceiptDTO>>(stockReceipts));
        }

        [HttpGet("GetStockReceiptById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<StockReceipt> stockReceipts = _context.StockReceipts.Where(s => s.ReceiptId == id).ToList();
            return Ok(_mapper.Map<List<StockReceiptDTO>>(stockReceipts));
        }

        [HttpPost("AddStockReceipt")]
        public IActionResult Add(StockReceipt stockReceipts)
        {
            _context.StockReceipts.Add(stockReceipts);
            _context.SaveChanges();
            /* return Ok(_mapper.Map<List<ProductDTO>>(product));*/
            return Ok(stockReceipts);
        }

        [HttpPut("UpdateStockReceipt")]
        public IActionResult Update(StockReceipt stockReceipts, int id)
        {
            var stockreceipts = _context.StockReceipts.FirstOrDefault(p => p.ReceiptId == id);
            if (stockreceipts == null)
            {
                return NotFound();
            }

            stockreceipts.DateReceipt = stockReceipts.DateReceipt;
            stockreceipts.WarehouseId = stockReceipts.WarehouseId;
            stockreceipts.SupplierId = stockReceipts.SupplierId;

            _context.Entry(stockreceipts).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(stockreceipts);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            StockReceipt stockreceipts = _context.StockReceipts.FirstOrDefault(p => p.ReceiptId == id);
            if (stockreceipts == null)
            {
                return NotFound(id);
            }

            _context.StockReceipts.Remove(stockreceipts);
            _context.SaveChanges();
            return Ok(stockreceipts);
        }
    }
}
