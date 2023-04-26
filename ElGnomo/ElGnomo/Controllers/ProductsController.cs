using Microsoft.AspNetCore.Mvc;
using ElGnomoModels.ViewModels;
using ElGnomo.Utils;
using Microsoft.AspNetCore.Authorization;

namespace ElGnomo.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly APIServices _services;
        public ProductsController(APIServices services)
        {
            _services = services;
            _services.SetModule("Products");
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _services.Get<IEnumerable<ProductView>>();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
           var product = await _services.Get<ProductView>(id.ToString());
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductView product)
        {
            await _services.Post(product);
            return RedirectToAction("Index");

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _services.Get<ProductView>(id.ToString());
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductView product)
        {
            await _services.Put(product,product.Id.ToString());
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _services.Get<ProductView>(id.ToString());
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _services.Delete(id.ToString());
            return RedirectToAction("Index");
        }
    }
}
