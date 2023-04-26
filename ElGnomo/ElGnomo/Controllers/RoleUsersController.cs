using ElGnomo.Utils;
using ElGnomoModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            var usersAPIService = _services.SetModule("Users");
            var users = await usersAPIService.Get<IEnumerable<UserView>>();
            var rolesAPIService = _services.SetModule("Roles");
            var roles = await rolesAPIService.Get<IEnumerable<RoleView>>();

            RoleUsersView roleUsersView = new()
            {
                Users = users.Select(u => new SelectListItem()
                {
                    Value = u.Id.ToString(),
                    Text = u.Email
                }).ToList(),
                Roles = roles.Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList(),
                User = users.FirstOrDefault(),
                Role = roles.FirstOrDefault()
            };

            _services.SetModule("RoleUsers"); //Use module again
            return View(roleUsersView);
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
            var usersAPIService = _services.SetModule("Users");
            var users = await usersAPIService.Get<IEnumerable<UserView>>();
            var rolesAPIService = _services.SetModule("Roles");
            var roles = await rolesAPIService.Get<IEnumerable<RoleView>>();
            _services.SetModule("RoleUsers"); //Use module again
            var roleUser = await _services.Get<RoleUsersView>(id.ToString());

            roleUser.Users = users.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Email,
                Selected = u.Id == roleUser.UserId
            }).ToList();

            roleUser.Roles = roles.Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name,
                Selected = r.Id == roleUser.RoleId
            }).ToList();

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
