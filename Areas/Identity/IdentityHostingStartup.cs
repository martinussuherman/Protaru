using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonevAtr.Areas.Identity.Data;

[assembly : HostingStartup(typeof(MonevAtr.Areas.Identity.IdentityHostingStartup))]
namespace MonevAtr.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            _ = builder.ConfigureServices((context, services) =>
            {
                _ = services.AddDbContext<MonevAtrIdentityDbContext>(options =>
                {
                    // _ = options.UseMySql(context.Configuration.GetConnectionString("MonevAtr"));
                    _ = options.UseSqlite(
                        context.Configuration.GetConnectionString("MonevAtrIdentity"));
                });

                _ = services
                    .AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<MonevAtrIdentityDbContext>();
            });
        }
    }
}