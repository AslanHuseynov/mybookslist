using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mybookslist.Areas.Identity.Data;
using mybookslist.Data;

[assembly: HostingStartup(typeof(mybookslist.Areas.Identity.IdentityHostingStartup))]
namespace mybookslist.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<mybookslistDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("mybookslistDbContextConnection")));
                
                    services.AddDefaultIdentity<mybookslistUser>(options =>
                    { 
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    })
                    .AddEntityFrameworkStores<mybookslistDbContext>();
            });
        }
    }
}