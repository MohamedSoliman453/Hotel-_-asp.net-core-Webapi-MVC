using Hotel.MVC.DTO;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.MVC.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDto)
        {
            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.PostAsJsonAsync("api/account/register", registerUserDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                // Handle API error response here
                // Log the error, parse error message, etc.
                return View(registerUserDto);
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUserDto);
            }

            var client = _clientFactory.CreateClient("BookingAPI");
            var response = await client.PostAsJsonAsync("api/account/login", loginUserDto);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                // Store the token in an HTTP-only cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = tokenResponse.Expiration
                };
                Response.Cookies.Append("JWTToken", tokenResponse.Token, cookieOptions);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle login failure
                ViewBag.ErrorMessage = "Invalid login attempt. Please try again.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            // Remove the JWT token cookie
            Response.Cookies.Delete("JWTToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true // if using HTTPS
            });

            // Redirect the user to the homepage or login page after logout
            return RedirectToAction("Index", "Home");
        }

    }
}
