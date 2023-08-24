using API.DTO;
using API.Models;
using FE_Client.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;

namespace PetStoreClient.Controllers
{
    [Authorize(Roles = "MANAGER")]
    public class ManagerController : Controller
    {
        private readonly HttpClient _client = null;
        private string DefaultApiUrl = "";
        private string DefaultApiUrlCusomer = "";
        private string DefaultApiUrlProductList = "";
        private string DefaultApiUrlProductDetail = "";
        private string DefaultApiUrlEmployee = "";
        private string DefaultApiUrlWareHouse = "";
        private string DefaultApiUrlSupperlier = "";

        public ManagerController()
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            DefaultApiUrl = "";
            DefaultApiUrlCusomer = "http://localhost:5000/api/Customer";
            DefaultApiUrlProductList = "http://localhost:5000/api/Product";
            DefaultApiUrlProductDetail = "http://localhost:5000/api/Product/GetProductById";
            DefaultApiUrlEmployee = "http://localhost:5000/api/Manager";
            DefaultApiUrlWareHouse = "http://localhost:5000/api/Warehouse";
            DefaultApiUrlSupperlier = "http://localhost:5000/api/Supplier";

        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            //http://localhost:5000/api/Manager/manager%40gmail.com

            ClaimsPrincipal claimsPrincipal = HttpContext.User as ClaimsPrincipal;
            string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            HttpResponseMessage response = await _client.GetAsync(DefaultApiUrlEmployee + "/" + email);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize the JSON array into a list of AccountInfo objects
                List<AccountInfo> managerInfos = System.Text.Json.JsonSerializer.Deserialize<List<AccountInfo>>(responseContent, options);

                // Now you have a list of AccountInfo objects, you can process each item
                foreach (var managerInfo in managerInfos)
                {
                    managerInfo.UserName = managerInfo.UserName?.Trim();
                    managerInfo.Password = managerInfo.Password?.Trim();
                }

                return View(managerInfos);
            }

            return View();
        }

        public async Task<IActionResult> CustomerList(int page = 1, int pageSize = 10, string customerName = "")
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            HttpResponseMessage customerResponse;
            customerResponse = await _client.GetAsync(DefaultApiUrlCusomer);
            string strCustomer;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strCustomer = await customerResponse.Content.ReadAsStringAsync();
            List<CustomerDTO> listCustomers = System.Text.Json.JsonSerializer.Deserialize<List<CustomerDTO>>(strCustomer, options);

            if (!string.IsNullOrEmpty(customerName))
            {
                listCustomers = listCustomers.Where(c => c.CustomerName.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();
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

        public async Task<IActionResult> WareHouse(int page = 1, int pageSize = 10, string wareHouseName = "")
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            HttpResponseMessage wareHouseResponse;
            wareHouseResponse = await _client.GetAsync(DefaultApiUrlWareHouse + "/GetAllWareHouse");
            string strWareHouse;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strWareHouse = await wareHouseResponse.Content.ReadAsStringAsync();
            List<WarehouseDTO> listWareHouse = System.Text.Json.JsonSerializer.Deserialize<List<WarehouseDTO>>(strWareHouse, options);

            if (!string.IsNullOrEmpty(wareHouseName))
            {
                listWareHouse = listWareHouse.Where(c => c.WarehouseName.Contains(wareHouseName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalItems = listWareHouse.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int startIndex = (page - 1) * pageSize;
            List<WarehouseDTO> currentPageWareHouse = listWareHouse.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(currentPageWareHouse);
        }

        public async Task<IActionResult> SupperlierList(int page = 1, int pageSize = 10, string supperlierName = "")
        {
            //http://localhost:5000/api/Supplier/GetAllSupperlier
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            HttpResponseMessage supperlier;
            supperlier = await _client.GetAsync(DefaultApiUrlSupperlier + "/GetAllSupperlier");
            string strSupperlier;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strSupperlier = await supperlier.Content.ReadAsStringAsync();
            List<SupplierDTO> listSupperlier = System.Text.Json.JsonSerializer.Deserialize<List<SupplierDTO>>(strSupperlier, options);

            if (!string.IsNullOrEmpty(supperlierName))
            {
                listSupperlier = listSupperlier.Where(c => c.SuppliersName.Contains(supperlierName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalItems = listSupperlier.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int startIndex = (page - 1) * pageSize;
            List<SupplierDTO> currentSupperlier = listSupperlier.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(currentSupperlier);
        }

        public async Task<IActionResult> ProductList(int page = 1, int pageSize = 10, string productName = "")
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            HttpResponseMessage productResponse = await _client.GetAsync(DefaultApiUrlProductList + "/GetAllProduct");
            string strProduct = await productResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            strProduct = await productResponse.Content.ReadAsStringAsync();
            List<ProductDTO> listProducts = System.Text.Json.JsonSerializer.Deserialize<List<ProductDTO>>(strProduct, options);

            if (!string.IsNullOrEmpty(productName))
            {
                listProducts = listProducts.Where(c => c.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalItems = listProducts.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int startIndex = (page - 1) * pageSize;
            List<ProductDTO> currentPageProducts = listProducts.Skip(startIndex).Take(pageSize).ToList();

            // Adjust quantity by subtracting 500
            foreach (var product in currentPageProducts)
            {
                if (product.Quantity.HasValue) // Kiểm tra Quantity có giá trị không
                {
                    product.Quantity = Math.Max(product.Quantity.Value - 500, 0);
                }
                else
                {
                    // Xử lý khi Quantity là null, ví dụ:
                    product.Quantity = 0; // Gán giá trị mặc định
                }
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(currentPageProducts);
        }

        public async Task<IActionResult> EditProfile(EditProfileForm updateDto)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            ClaimsPrincipal claimsPrincipal = HttpContext.User as ClaimsPrincipal;
            string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            if(updateDto.Name == null || updateDto.Address == null || updateDto.Phone == null)
            {
                return View();
            }

            var json = NewtonsoftJson.SerializeObject(updateDto); 
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //http://localhost:5000/api/Manager/editProfile?email=manager%40gmail.com
            HttpResponseMessage response = await _client.PutAsync(DefaultApiUrlEmployee + "/editProfile?email=" + email, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            //http://localhost:5000/api/Product/GetProductById/1
            HttpResponseMessage productDetailResponse = await _client.GetAsync(DefaultApiUrlProductDetail + "/" + productId);
            string strProductDetail;
            try
            {
               strProductDetail = await productDetailResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return View("Error");
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ProductDTO> productDetail = System.Text.Json.JsonSerializer.Deserialize<List<ProductDTO>>(strProductDetail, options);

            return View(productDetail);
        }

        public async Task<IActionResult> WareHouseDetail(int wareHouseId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            //http://localhost:5000/api/Warehouse/GetWareHouseById/1

            HttpResponseMessage productDetailResponse = await _client.GetAsync(DefaultApiUrlWareHouse + "/GetWareHouseById/" + wareHouseId);
            string strWareHouseDetail;
            try
            {
                strWareHouseDetail = await productDetailResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return View("Error");
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            WarehouseDTO wareHouseDetail = System.Text.Json.JsonSerializer.Deserialize<WarehouseDTO>(strWareHouseDetail, options);

            return View(wareHouseDetail);
        }


        public async Task<IActionResult> AddProduct(ProductDTO productForm)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            HttpResponseMessage categoryResponse = await _client.GetAsync("http://localhost:5000/api/Categories");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categories = await categoryResponse.Content.ReadFromJsonAsync<List<CategoriesDTO>>();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            }
            //http://localhost:5000/api/Warehouse/GetAllWareHouse
            HttpResponseMessage warehouseResponse = await _client.GetAsync("http://localhost:5000/api/Warehouse/GetAllWareHouse");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var warehouses = await warehouseResponse.Content.ReadFromJsonAsync<List<WarehouseDTO>>();
                ViewBag.Warehoues = new SelectList(warehouses, "WarehouseId", "WarehouseName");
            }
            //http://localhost:5000/api/Supplier/GetAllSupperlier
            HttpResponseMessage supperlierResponse = await _client.GetAsync("http://localhost:5000/api/Supplier/GetAllSupperlier");

            if (supperlierResponse.IsSuccessStatusCode)
            {
                var supperliers = await supperlierResponse.Content.ReadFromJsonAsync<List<SupplierDTO>>();
                ViewBag.Supperliers = new SelectList(supperliers, "SuppliersId", "SuppliersName");
            }

            if (productForm.ProductName == null || productForm.Describe == null || productForm.SuppliersId == null)
            {
                ViewBag.ErrorMessage = "Product data is missing.";
                return View(); 
            }

            var json = NewtonsoftJson.SerializeObject(productForm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(DefaultApiUrlProductList + "/AddProduct", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Product add is sussess.";
                return View();
            }

            return View();
        }

        public async Task<IActionResult> AddWareHouse(Warehouse wareHouseForm)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            if (wareHouseForm.WarehouseName == null || wareHouseForm.Address == null )
            {
                ViewBag.ErrorMessage = "WareHouse data is missing.";
                return View();
            }
            //http://localhost:5000/api/Warehouse/AddWareHouse

            var json = NewtonsoftJson.SerializeObject(wareHouseForm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(DefaultApiUrlWareHouse + "/AddWareHouse", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "WareHouse add is sussess.";
                return View();
            }

            return View();
        }

        public async Task<IActionResult> AddSupperlier(Supplier supplierForm)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            if (supplierForm.SuppliersName == null || supplierForm.Address == null)
            {
                ViewBag.ErrorMessage = "Supplier data is missing.";
                return View();
            }
            //http://localhost:5000/api/Supplier/AddSupperlier

            var json = NewtonsoftJson.SerializeObject(supplierForm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(DefaultApiUrlSupperlier + "/AddSupperlier", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Supplier add is sussess.";
                return View();
            }

            return View();
        }

        public async Task<IActionResult> DeleteWareHouse(int wareHouseId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");
            //http://localhost:5000/api/Warehouse/DeleteWareHouseById?id=4
            HttpResponseMessage deleteResponse = await _client.DeleteAsync(DefaultApiUrlWareHouse + "/DeleteWareHouseById?id=" + wareHouseId);

            if (!deleteResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return Redirect("/Manager/WareHouse");
        }

        public async Task<IActionResult> DeleteSuppliers(int suppliersId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            //http://localhost:5000/api/Warehouse/DeleteWareHouseById?id=4
            HttpResponseMessage deleteResponse = await _client.DeleteAsync(DefaultApiUrlSupperlier + "/DeleteSupperlierById?id=" + suppliersId);

            if (!deleteResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return Redirect("/Manager/SupperlierList");
        }


        public async Task<IActionResult> SetActiveAccount(int customerId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            var request = new HttpRequestMessage(HttpMethod.Put, DefaultApiUrlCusomer + "/ToggleActive/" + customerId);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return View();
            }

            return View();
        }


        public IActionResult Dasbroad()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Index", "Login");

            // Các xử lý liên quan đến trang Dasbroad ở đây
            // Ví dụ: bạn có thể chuẩn bị dữ liệu cần thiết cho trang và trả về View

            return View();
        }
    }
}
