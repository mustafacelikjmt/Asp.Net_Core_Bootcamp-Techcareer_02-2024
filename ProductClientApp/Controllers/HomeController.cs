using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductClientApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ProductClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = new List<ProductDTO>();

            var token = User.Claims.FirstOrDefault(x => x.Type == "JWToken")?.Value;
            if (token == null)
            {
                return RedirectToAction("Login", "Home");
            }

            using (var httpClient = new HttpClient())
            {
                // Token deðerini dinamik olarak deðiþtirebilirsiniz.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("http://localhost:5172/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<ProductDTO>>(apiResponse);
                }
            }
            foreach (var item in products)
            {
                Console.WriteLine(item);
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5172/api/Users/login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtTokenModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();
                    // Expiration tarihini Unix timestamp'ten DateTime'a çevir
                    var expireUnixTimestamp = long.Parse(token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value);
                    var expireDate = DateTimeOffset.FromUnixTimeSeconds(expireUnixTimestamp).UtcDateTime;
                    tokenModel.ExpireDate = expireDate;
                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("JWToken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
    }
}