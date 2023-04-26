using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElGnomo.Controllers
{
    public class RoleUsersController : Controller
    {
        private readonly APIServices _services;
        public RoleUsersController(APIServices services)
        {
            _services = services;
            _services.SetModule("RoleUsers");
        }

        public async Task<IActionResult> Index()
        {
            var roleUsers = await _services.Get<IEnumerable<RoleUsersView>>();
            return View(roleUsers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var roleUser = await _services.Get<RoleUsersView>(id.ToString());
            return View(roleUser);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleUsersView roleUser)
        {
            await _services.Post(roleUser);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var roleUser = await _services.Get<RoleUsersView>(id.ToString());
            return View(roleUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleUsersView roleUser)
        {
            await _services.Put(roleUser, roleUser.Id.ToString());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var roleUser = await _services.Get<RoleUsersView>(id.ToString());
            return View(roleUser);
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
