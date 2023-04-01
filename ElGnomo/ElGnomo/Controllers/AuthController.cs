using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();
            user.PasswordHash = Cypher.CypherText(user.Password);

            var result = await _services.Post<bool>(user, "login");
            if (!result) return View();

            return RedirectToAction("Index", "Home");
        }
    }
}
