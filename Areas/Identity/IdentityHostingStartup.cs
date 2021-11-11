using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using urun_katalog.Areas.Identity.Data;

[assembly: HostingStartup(typeof(urun_katalog.Areas.Identity.IdentityHostingStartup))]
namespace urun_katalog.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDataContextConnection")));
                services.AddIdentity<IdentityUser, IdentityRole>()
                              .AddDefaultUI()
                              .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}