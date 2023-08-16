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

        [HttpGet("GetSProductById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<Product> products = _context.Products.Where(s => s.ProductId == id).ToList();
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpPost("AddProduct")]
        public IActionResult Add(Product product)
        {
                _context.Products.Add(product);
                 _context.SaveChanges();           
            /* return Ok(_mapper.Map<List<ProductDTO>>(product));*/
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

        [HttpDelete]
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



    }
}
