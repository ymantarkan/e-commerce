using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using urun_katalog.Areas.Identity.Data;
using urun_katalog.Models;

namespace urun_katalog.Controllers.Admin.Controllers
{
     [Authorize(Roles ="Super user")]
  
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;

        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;

        }
        public IActionResult Index()
        {
            return View(_db.products.Include(c => c.Category).Include(f => f.SpecialTag).ToList());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.products.Include(c => c.Category).Include(c => c.SpecialTag)
                        .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();

            if (lowAmount == null || largeAmount == null)
            {
                products = _db.products.Include(c => c.Category).Include(c => c.SpecialTag).ToList();


            }


            return View(products);
        }
 
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.categories.ToList(), "Id", "ProductName");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.products.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "Bu ürün zaten var ";
                    ViewData["productTypeId"] = new SelectList(_db.categories.ToList(), "Id", "ProductName");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(product);

                }
                if (image != null)
                {

                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;


                }
                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(product);

        }

        public IActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.categories.ToList(), "Id", "ProductName");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).Include(c => c.SpecialTag)
                        .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;


                }
                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);

        }
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).Include(c => c.SpecialTag)
                        .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).Include(c => c.SpecialTag)
                        .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).Include(c => c.SpecialTag)
                        .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }




    }
}