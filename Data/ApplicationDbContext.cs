using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using urun_katalog.Models;

namespace urun_katalog.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> categories{get;set;}
         public DbSet<SpecialTag> SpecialTags { get; set; }
         public DbSet<Product> products{get;set;}
         public DbSet<Order> Orders{get;set;}
         public DbSet<OrderDetails> OrderDetails{get;set;}
         public DbSet<ApplicationUser> ApplicationUsers{get;set;}

    
    }
}
