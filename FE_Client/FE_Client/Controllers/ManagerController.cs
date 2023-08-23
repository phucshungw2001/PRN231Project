﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace PetStoreClient.Controllers
{
    [Authorize(Roles = "MANAGER")]
    public class ManagerController : Controller
    {
        private readonly HttpClient _client = null;
        private string DefaultApiUrl = "";
        private string DefaultApiUrlCusomer = "";
        private string DefaultApiUrlProductList = "";
        private string DefaultApiUrlEmployee = "";

        public ManagerController()
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            DefaultApiUrl = "https://localhost:7001/api/Account";
            DefaultApiUrlCusomer = "https://localhost:7001/api/Customer";
            DefaultApiUrlProductList = "https://localhost:7001/api/Product";
            DefaultApiUrlEmployee = "https://localhost:7001/api/Employee";

        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToAction("Index", "Login");

            ClaimsPrincipal claimsPrincipal = HttpContext.User as ClaimsPrincipal;
            string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            HttpResponseMessage response = await _client.GetAsync(DefaultApiUrl + "/Infomation?email=" + email);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                AccountDTO accountInfo = JsonSerializer.Deserialize<AccountDTO>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var jsonDocument = JsonDocument.Parse(responseContent);

                string employeeId = jsonDocument.RootElement.GetProperty("employeeId").GetString();

                string employeeUrl = DefaultApiUrlEmployee + "/id?id=" + employeeId;
                HttpResponseMessage employeeResponse = await _client.GetAsync(employeeUrl);

                if (employeeResponse.IsSuccessStatusCode)
                {
                    string employeeContent = await employeeResponse.Content.ReadAsStringAsync();
                    EmployeeDTO employeeInfo = JsonSerializer.Deserialize<EmployeeDTO>(employeeContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var viewModel = new AccountViewModel
                    {
                        Account = accountInfo,
                        Employee = employeeInfo
                    };
                    return View(viewModel);
                }
            }
            return View();
        }
        public class AccountViewModel
        {
            public AccountDTO Account { get; set; }
            public EmployeeDTO Employee { get; set; }
        }

        public async Task<IActionResult> CustomerList(int page = 1, int pageSize = 10, string customerName = "")
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToAction("Index", "Login");
            HttpResponseMessage customerResponse;
            customerResponse = await _client.GetAsync(DefaultApiUrlCusomer);
            string strCustomer;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strCustomer = await customerResponse.Content.ReadAsStringAsync();
            List<CustomerDTO> listCustomers = JsonSerializer.Deserialize<List<CustomerDTO>>(strCustomer, options);

            if (!string.IsNullOrEmpty(customerName))
            {
                listCustomers = listCustomers.Where(c => c.Name.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalItems = listCustomers.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int startIndex = (page - 1) * pageSize;
            List<CustomerDTO> currentPageCustomer = listCustomers.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(currentPageCustomer);
        }

        public async Task<IActionResult> ProductList(int page = 1, int pageSize = 10, string productName = "")
        {
            if (HttpContext.Session.GetString("PetSession") == null)
                return RedirectToAction("Index", "Login");
            HttpResponseMessage productResponse;
            productResponse = await _client.GetAsync(DefaultApiUrlProductList);
            string strProduct;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strProduct = await productResponse.Content.ReadAsStringAsync();
            List<ProductDTO> listProducts = JsonSerializer.Deserialize<List<ProductDTO>>(strProduct, options);

            if (!string.IsNullOrEmpty(productName))
            {
                listProducts = listProducts.Where(c => c.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalItems = listProducts.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int startIndex = (page - 1) * pageSize;
            List<ProductDTO> currentPageCustomer = listProducts.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(currentPageCustomer);
        }
    }
}
