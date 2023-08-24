using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

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
        public IActionResult GetAllWareHouse()
        {
            List<Warehouse> warehouses = _context.Warehouses.Include(w => w.Products).ToList();
            List<WarehouseDTO> warehouseDTOs = _mapper.Map<List<WarehouseDTO>>(warehouses);

            return Ok(warehouseDTOs);
        }

        [HttpGet("GetWareHouseById/{id}")]
        public IActionResult GetbyId(int id)
        {
            Warehouse warehouses = _context.Warehouses.FirstOrDefault(s => s.WarehouseId == id);

            List<Product> proudct = warehouses.Products.ToList();
            WarehouseDTO warehouseDTO = _mapper.Map<WarehouseDTO>(warehouses);
            warehouseDTO.Products = _mapper.Map<List<ProductDTO>>(proudct);

            return Ok(warehouseDTO);
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
