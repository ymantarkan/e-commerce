using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using urun_katalog.Areas.Identity.Data;
using urun_katalog.Models;

namespace urun_katalog.Controllers.Admin.Controllers
{
    [Authorize(Roles = "Super user")]
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _db;

        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;


        }

        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<IActionResult> Create(SpecialTag specialTag)
        {

            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag){

            if(ModelState.IsValid){
                _db.Update(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(specialTag);
        }
          public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTag specialTag)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != specialTag.Id)
            {
                return NotFound();
            }

            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }





    }
}