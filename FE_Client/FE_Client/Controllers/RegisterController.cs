using API.DTO;
using FE_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FE_Client.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5000/api/Account/registerDto";

        public RegisterController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var registerDto = new RegisterDTO
                {
                    UserName = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    CustomerName = model.Name,
                    Address = model.Address
                };
                var json = JsonSerializer.Serialize(registerDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl, content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("RegistrationSuccessful");
            }
            catch (HttpRequestException)
            {
                // Handle API request error
                ModelState.AddModelError(string.Empty, "Error occurred during registration. Please try again later.");
                return View(model);
            }
        }

        public IActionResult RegistrationSuccessful()
        {
            return View();
        }
    }
}
