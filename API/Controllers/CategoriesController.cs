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
    public class CategoriesController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public CategoriesController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Category> categories = _context.Categories.ToList();
            return Ok(_mapper.Map<List<CategoriesDTO>>(categories));
        }

        [HttpGet("{categoryId}")]
        public IActionResult Get(int categoryId)
        {
            List<Category> categories = _context.Categories.Where(c => c.CategoryId == categoryId).ToList();
            return Ok(_mapper.Map<List<CategoriesDTO>>(categories));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategories(CategoriesDTO categoriesDTO)
        {
            if (categoriesDTO == null)
            {
                return BadRequest("Categories data is missing.");
            }

            var newCategories = new Category
            {
                CategoryId = categoriesDTO.CategoryId,
                CategoryName = categoriesDTO.CategoryName,
            };

            _context.Categories.Add(newCategories);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<CategoriesDTO>(newCategories));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPut("UpdateCategories")]
        public IActionResult Update(CategoriesDTO categories, int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = categories.CategoryName;

            try
            {
                _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            return Ok(category);
        }

        [HttpDelete]
        public IActionResult Delete(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(p => p.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            return Ok(category);
        }

    }
}
