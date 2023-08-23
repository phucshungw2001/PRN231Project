using API.DTO;
using API.Form;
using API.Models;
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
    public class ManagerController : ControllerBase
    {
        public WarehousesContext _context;
        private IMapper _mapper;
        public ManagerController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Manager> managers = _context.Managers.ToList();
            return Ok(_mapper.Map<List<ManagerDTO>>(managers));
        }

        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            List<Account> accounts = _context.Accounts.Include(a => a.Manager).Where(a => a.UserName == email).ToList();
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

                var existingAccount = await _context.Accounts.SingleOrDefaultAsync(a => a.UserName == email);
                if (existingAccount == null)
                {
                    return NotFound("Account not found.");
                }

                existingAccount.Manager.Phone = updateDto.Phone;
                existingAccount.Manager.ManagerName = updateDto.Name;
                existingAccount.Manager.Address = updateDto.Address;

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
