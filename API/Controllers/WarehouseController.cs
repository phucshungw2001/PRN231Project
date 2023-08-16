using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public WarehouseController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllWareHouse")]
        public IActionResult Get()
        {
            List<Warehouse> warehouses = _context.Warehouses.ToList();
            return Ok(_mapper.Map<List<WarehouseDTO>>(warehouses));
        }

        [HttpGet("GetWareHouseById/{id}")]
        public IActionResult GetbyId(int id)
        {
            List<Warehouse> warehouses = _context.Warehouses.Where(s => s.WarehouseId == id).ToList();
            return Ok(_mapper.Map<List<WarehouseDTO>>(warehouses));
        }

        [HttpPost("AddWareHouse")]
        public IActionResult Add(Warehouse warehouse)
        {
            try
            {
                _context.Warehouses.Add(warehouse);
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
