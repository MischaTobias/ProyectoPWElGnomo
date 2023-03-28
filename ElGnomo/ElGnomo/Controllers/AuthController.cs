using ElGnomo.Models;
using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElGnomo.Controllers
{
    public class AuthController : Controller
    {
        private readonly ElgnomoContext _context;
        public AuthController()
        {
            _context = new ElgnomoContext();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password != user.ConfirmPassword) return View();
            User newUser = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = Cypher.CypherText(user.Password)
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();

            var encryptedPassword = Cypher.CypherText(user.Password);
            var exists = _context.Users.Where(u => u.Email == user.Email && u.PasswordHash == encryptedPassword).Any();
            if(!exists) return View();

            return View();
        }
    }
}
