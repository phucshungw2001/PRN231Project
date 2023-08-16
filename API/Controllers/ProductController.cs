using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
