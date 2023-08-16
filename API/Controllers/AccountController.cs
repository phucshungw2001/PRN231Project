using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private WarehousesContext _context;
        private IMapper _mapper;

        public AccountController(WarehousesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(loginInfo.UserName) ||
                    string.IsNullOrEmpty(loginInfo.Password))
                {
                    throw new ApplicationException("Login Information is invalid!! Please check again...");
                }

                Account loginAccount = _context.Accounts.SingleOrDefault(x => x.UserName == loginInfo.UserName && x.Password == loginInfo.Password);
                if (loginAccount == null)
                {
                    throw new ApplicationException("Failed to login! Please check the information again...");
                }

                AccountDTO loginAccountDTO = _mapper.Map<AccountDTO>(loginAccount);
                return Ok(loginAccountDTO); 
            }
            catch (ApplicationException ae)
            {
                return BadRequest(ae.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }


        [HttpPost("registerDto")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Accounts.Any(a => a.UserName == registerDto.UserName))
            {
                return Conflict("This email is already registered.");
            }

            var newCustomer = new Customer
            {
                Phone = registerDto.Phone,
                CustomerName = registerDto.CustomerName,
                Address = registerDto.Address
            };

            var newAccount = new Account
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Customer = newCustomer,
                Role = registerDto.Role,
                IsActive = registerDto.IsActive
            };

            await _context.Customers.AddAsync(newCustomer);
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            return Ok("Registration successful!");
        }
    }
}
