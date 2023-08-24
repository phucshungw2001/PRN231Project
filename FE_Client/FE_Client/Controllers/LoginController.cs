using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using FE_Client.Form;
using Newtonsoft.Json;

namespace PetStoreClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;
        private string DefaultApiUrl = "";
        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DefaultApiUrl = "http://localhost:5000/api/Account";
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm, Bind("Email", "Password")] LoginForm loginInfo)
        {
            try
            {
                var json = JsonConvert.SerializeObject(loginInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //http://localhost:5000/api/Account/customer01%40gmail.com
                //http://localhost:5000/api/Account/GetActive/customer01%40gmail.com
                HttpResponseMessage roleResponse = await client.GetAsync(DefaultApiUrl + "/" + loginInfo.Email);
                HttpResponseMessage response = await client.PostAsync(DefaultApiUrl + "/login", content);
                HttpResponseMessage responseIsActive = await client.PostAsync(DefaultApiUrl + "/GetActive/" + loginInfo.Email);

                if (response.IsSuccessStatusCode)
                {
                    var roleContent = await roleResponse.Content.ReadAsStringAsync();
                    var isActiveContent = await responseIsActive.Content.ReadAsStringAsync();
                    string role = roleContent.Trim(' ', '\"');
                    bool isActive = isActiveContent;
                    if (!isActive)
                    {
                        ViewBag.ErrorMessage = "Tài khoản bị khoá";
                        return View();
                    }

                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Email, loginInfo.Email),
                       new Claim(ClaimTypes.Role, role.ToString())
                    };

                    // Create claims identity
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("UserSession", loginInfo.Email);
                    // Redirect based on role
                    if (role.ToString() == "MANAGER")
                    {
                        return Redirect("/Manager/index");
                    }
                    else if (role.ToString() == "USER")
                    {
                        return Redirect("/Home/index");
                    }
                    return View(loginInfo);
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                ViewData["Login"] = ex.Message;
                return View();
            }
        }

    }
}
