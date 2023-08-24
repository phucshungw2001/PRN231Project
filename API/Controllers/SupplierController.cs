using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public SupplierController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllSupperlier")]
        public IActionResult Get()
        {
            List<Supplier> suppliers = _context.Suppliers.ToList(); 
            return Ok(_mapper.Map<List<SupplierDTO>>(suppliers));
        }

        [HttpGet("GetSupperlierById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<Supplier> suppliers = _context.Suppliers.Where(s => s.SuppliersId == id).ToList();
            return Ok(_mapper.Map<List<SupplierDTO>>(suppliers));
        }

        [HttpDelete("DeleteSupperlierById")]
        public IActionResult Delete(int id)
        {
            Supplier supplier = _context.Suppliers.FirstOrDefault(w => w.SuppliersId == id);
            if (supplier == null)
            {
                return NotFound(id);
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return Ok(supplier);
        }

        [HttpPost("AddSupperlier")]
        public IActionResult Add(Supplier supplier) 
        {
            try
            {
                _context.Suppliers.Add(supplier);
                int result = _context.SaveChanges();
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
