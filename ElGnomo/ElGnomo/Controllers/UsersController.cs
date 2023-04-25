using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElGnomo.Controllers
{
    public class UsersController : Controller
    {
        private readonly APIServices _services = new();

        public UsersController()
        {
            _services.SetModule("Users");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _services.Get<IEnumerable<UserView>>();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _services.Get<UserView>(id.ToString());
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();
            user.PasswordHash = Cypher.CypherText(user.Password);
            await _services.Post(user);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _services.Get<UserView>(id.ToString());
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserView user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) return View();
            user.PasswordHash = Cypher.CypherText(user.Password);
            await _services.Put(user, user.Id.ToString());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _services.Get<UserView>(id.ToString());
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction("Index");
        }
    }
}
