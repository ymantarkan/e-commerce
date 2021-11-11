using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace urun_katalog.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails=new List<OrderDetails>();
            
        }
        public int Id { get; set; }
        [Display(Name="Order No")]
        public string OrderNo { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Please enter email Adress")]
        public string Email { get; set; }

        [Required]
        public string Adress { get; set; }

        public DateTime OrderDate{get;set;}

        public virtual List<OrderDetails> OrderDetails{get;set;}

        
    }
}