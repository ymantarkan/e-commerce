
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using urun_katalog.Areas.Identity.Data;
using urun_katalog.Models;

namespace urun_katalog.Controllers.Admin.Controllers
{
 

      [Authorize(Roles ="Super user")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_db.categories.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Add(category);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Category category)
        {
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Category category)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != category.Id)
            {
                return NotFound();
            }

            var category1 = _db.categories.Find(id);
            if (category1 == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(category1);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

















    }
}