using API.DTO;
using API.Form;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private WarehousesContext _context;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(WarehousesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Account> account = _context.Accounts.ToList();
            return Ok(_mapper.Map<List<AccountDTO>>(account));
        }

        [HttpGet("{email}")]
        public IActionResult GetAccount(string email)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.UserName == email);
            return Ok(account.Role);
        }

        [HttpGet("GetActive/{userName}")]
        public IActionResult GetAccountIsActive(string userName)
        {
            Account account = _context.Accounts.FirstOrDefault(a => a.UserName == userName);
            return Ok(account.IsActive);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginForm login)
        {

            var result = _context.Accounts.SingleOrDefault(x => x.UserName == login.Email && x.Password == login.Password);

            if (result != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(ClaimTypes.Role, result.Role),
                 };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            else
            {
                return BadRequest("Invalid login attempt.");
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
                Role = "USER",
                IsActive = true
            };

            await _context.Customers.AddAsync(newCustomer);
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            return Ok("Registration successful!");
        }

    }
}
