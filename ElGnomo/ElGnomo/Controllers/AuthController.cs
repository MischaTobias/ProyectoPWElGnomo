using ElGnomo.Models;
using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElGnomo.Controllers
{
    public class AuthController : Controller
    {
        private readonly APIServices _services = new();
        public AuthController()
        {
            _services.SetModule("Auth");
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null) return RedirectToAction("Products");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password != user.ConfirmPassword) return View();
            user.PasswordHash = Cypher.CypherText(user.Password);

            var result = await _services.Post<bool>(user, "register");
            if (!result) return View();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null) return RedirectToAction("Products");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();
            user.PasswordHash = Cypher.CypherText(user.Password);

            var result = await _services.Post<bool>(user, "login");
            if (!result) return View();

            return RedirectToAction("Index", "Products");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
