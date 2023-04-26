using ElGnomo.Models;
using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ElGnomo.Controllers
{
    public class AuthController : Controller
    {
        private readonly APIServices _services;
        public AuthController(APIServices services)
        {
            _services = services;
            _services.SetModule("Auth");
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null) return RedirectToAction("Index", "Products");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password != user.ConfirmPassword) return View();

            var result = await _services.Post<TokenView>(user, "register");
            if (result == null) return View();

            if (result == null) return View();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Token", result.Token);

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Token", result.Token)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null) return RedirectToAction("Index", "Products");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();

            var result = await _services.Post<TokenView>(user, "login");
            if (result == null) return View();

            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Token", result.Token);

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Token", result.Token)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Index", "Products");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
