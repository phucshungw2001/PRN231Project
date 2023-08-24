using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public ProductController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllProduct")]
        public IActionResult Get()
        {
            List<Product> products = _context.Products.ToList();
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<Product> products = _context.Products.Where(s => s.ProductId == id).ToList();
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpGet("GetProductByProductName")]
        public IActionResult GetProductByProductName(string productName)
        {
            List<Product> products = _context.Products.Where(p => p.ProductName.Contains(productName)).ToList();

            if (products.Count == 0)
            {
                return NotFound("No products found with the given product name.");
            }

            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpGet("GetQuantityHistory")]
        public IActionResult GetQuantityHistory(int productId)
        {
            var history = _context.QuantityChangeHistories
                .Where(h => h.ProductId == productId)
                .OrderByDescending(h => h.Date)
                .ToList();

            return Ok(history);
        }

        [HttpPost("AddProduct")]
        public IActionResult Add(Product product)
        {
                _context.Products.Add(product);
                 _context.SaveChanges();           

            return Ok(product);
               
        }

        [HttpPut("UpdateProduct")]
        public IActionResult Update(Product product, int id) 
        {
            var products = _context.Products.FirstOrDefault(p => p.ProductId == id);    
            if (products == null)
            {
                return NotFound();
            }

            products.ProductName = product.ProductName;
            products.Describe = product.Describe;
            products.Quantity = product.Quantity;
            products.Price = product.Price;
            product.WarehouseId = product.WarehouseId;
            products.CategoryId = product.CategoryId;
            products.Status = product.Status;
            products.SuppliersId = product.SuppliersId;

            _context.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(products);
        }

        [HttpPut("UpdateQuantityProduct")]
        public IActionResult UpdateQuantityProduct(int productId, int newQuantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            var oldQuantity = product.Quantity;
            product.Quantity += newQuantity;

            // Thêm thông tin vào lịch sử
            AddQuantityHistoryEntry(productId, "Update", newQuantity);

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            var resultDTO = new QuantityUpdateResultDTO
            {
                NewQuantity = newQuantity,
                ExecutionTime = DateTime.UtcNow
            };

            return Ok(resultDTO);
        }

        [HttpPut("{ChangeStatusProductId}")]
        public async Task<IActionResult> UpdateOrderStatus(int ChangeStatusProductId)
        {
            var product = await _context.Products.FindAsync(ChangeStatusProductId);

            if (product == null)
            {
                return NotFound();
            }

            product.Status = !product.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpDelete("DeleteProductById")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound(id);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("DeleteQuantityProduct")]
        public IActionResult DeleteQuantityProduct(int productId, int deletedQuantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            if (deletedQuantity > 0 && deletedQuantity <= product.Quantity)
            {
                var oldQuantity = product.Quantity;
                product.Quantity -= deletedQuantity;

                // Thêm thông tin vào lịch sử
                AddQuantityHistoryEntry(productId, "Delete", deletedQuantity);

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();

                var resultDTO = new QuantityUpdateResultDTO
                {
                    NewQuantity = -deletedQuantity,
                    ExecutionTime = DateTime.UtcNow
                };

                return Ok(resultDTO);
            }
            else
            {
                return BadRequest("Deleted quantity exceeds current quantity.");
            }
        }

        private void AddQuantityHistoryEntry(int productId, string action, int change)
        {
            // Tạo một đối tượng lịch sử
            var historyEntry = new QuantityChangeHistory
            {
                ProductId = productId,
                Action = action,
                Change = change,
                Date = DateTime.UtcNow
            };
            // Thêm vào cơ sở dữ liệu hoặc cơ chế lưu trữ khác
            _context.QuantityChangeHistories.Add(historyEntry);
            _context.SaveChanges();
        }

    }
}
