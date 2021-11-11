using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using urun_katalog.Areas.Identity.Data;
using urun_katalog.Models;
using urun_katalog.Utility;

namespace urun_katalog.Controllers.Customer.Controllers
{
     // [Authorize(Roles = "Custumer")]
    public class OrderController:Controller
    {

        private ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db=db;
            
        }
        public IActionResult Checkout(){
            return View();
        }

        //Post checkout action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder){
          List<Product> products=HttpContext.Session.Get<List<Product>>("products");
          if(products!=null){
              foreach(var product in products){
                  OrderDetails orderDetails=new OrderDetails();
                  orderDetails.ProductId=product.Id;
                  anOrder.OrderDetails.Add(orderDetails);
              }
          }
          anOrder.OrderNo=GetOrderNo();
          _db.Orders.Add(anOrder);
          await _db.SaveChangesAsync();
          HttpContext.Session.Set("products",new List<Product>());
          return View();

            
        }

        public string GetOrderNo(){
            int rowCount=_db.Orders.ToList().Count()+1;
            return rowCount.ToString("000");
        }


        
    }
}