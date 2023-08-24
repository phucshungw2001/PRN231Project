using API.DTO;
using API.Form;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public CustomerController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Customer> customers = _context.Customers.ToList();
            return Ok(_mapper.Map<List<CustomerDTO>>(customers));
        }

        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            List<Account> accounts = _context.Accounts.Include(a => a.Customer).Where(a => a.UserName == email).ToList();  
            return Ok(_mapper.Map<List<AccountInfo>>(accounts));
        }

        [HttpPut("editProfile")]
        public async Task<IActionResult> EditProfile(string email, [FromBody] EditProfileForm updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingAccount = await _context.Accounts.Include(a => a.Customer).SingleOrDefaultAsync(a => a.UserName == email);
                if (existingAccount == null)
                {
                    return NotFound("Account not found.");
                }

                existingAccount.Customer.CustomerName = updateDto.Name;
                existingAccount.Customer.Address = updateDto.Address;
                existingAccount.Customer.Phone = updateDto.Phone;

                _context.Accounts.Update(existingAccount);
                await _context.SaveChangesAsync();

                return Ok("Profile updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
