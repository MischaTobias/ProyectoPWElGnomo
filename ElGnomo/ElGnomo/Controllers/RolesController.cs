using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElGnomo.Models;
using ElGnomoModels.ViewModels;
using ElGnomo.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ElGnomo.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly APIServices _services;
        public RolesController(APIServices services)
        {
            _services = services;
            _services.SetModule("Roles");
        }
        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _services.Get<IEnumerable<RoleView>>();
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var role = await _services.Get<RoleView>(id.ToString());
            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleView role)
        {
            await _services.Post(role);
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _services.Get<RoleView>(id.ToString());
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleView role)
        {
            await _services.Put(role, role.Id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _services.Get<RoleView>(id.ToString());
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> GetRoleJson()
        {
            var roleId = Convert.ToInt64(HttpContext.Request.Form["roleId"].First()?.ToString());
            var role = await _services.Get<RoleView>(roleId.ToString());
            return Json(role);
        }
    }
}
