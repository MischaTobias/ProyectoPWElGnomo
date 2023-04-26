using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElGnomo.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly APIServices _services;

        public UsersController(APIServices services)
        {
            _services = services;
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

        [HttpPost]
        public async Task<JsonResult> GetUserJson()
        {
            var userId = Convert.ToInt64(HttpContext.Request.Form["userId"].First()?.ToString());
            var user = await _services.Get<UserView>(userId.ToString());
            return Json(user);
        }
    }
}
