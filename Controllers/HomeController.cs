using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using urun_katalog.Areas.Identity.Data;
using urun_katalog.Models;
using urun_katalog.Utility;

namespace urun_katalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext dbContext, ILogger<HomeController> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _db.products.Include(c => c.Category).Include(c => c.SpecialTag).ToList();
            return View(_db.products.Include(c => c.Category).Include(c => c.SpecialTag).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Get method for Detail

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Detail")]
        public IActionResult ProductDetail(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (products != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));


        }
        //GET REMOVE ACTION METHOD
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }

            return View(products);
        }
    }
}
