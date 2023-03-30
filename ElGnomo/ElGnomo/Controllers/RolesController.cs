using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElGnomo.Models;
using ElGnomoModels.ViewModels;

namespace ElGnomo.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public async Task<IActionResult> Index()
        {
            return View();

        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();

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
            return View();

        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();

        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleView role)
        {
            return View();

        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View();

        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();

        }
    }
}
