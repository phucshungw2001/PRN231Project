using API.DTO;
using FE_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FE_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client = null;
        private string DefaultApiUrlProductList = "http://localhost:5000/api/Product/GetAllProduct";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public IActionResult Index()
        {
            var productList = GetProductList().Result;
            return View(productList);
        }

        private async Task<List<ProductDTO>> GetProductList()
        {
            HttpResponseMessage productResponse = await _client.GetAsync(DefaultApiUrlProductList);
            string strProduct = await productResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ProductDTO> listProducts = JsonSerializer.Deserialize<List<ProductDTO>>(strProduct, options);

            // Adjust quantity to a maximum of 500
            foreach (var product in listProducts)
            {
                if (product.Quantity > 500)
                {
                    product.Quantity = 500;
                }
                else if (product.Quantity == 0) // Add 500 when Quantity is 0
                {
                    product.Quantity = 500;
                }
            }

            return listProducts;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
