using System;
using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Protaru.Identity;

namespace MonevAtr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();

            PermissionGlobalSetting.CustomClaimType = Permissions.CustomClaimTypes;
            PermissionGlobalSetting.SuperPermission = Permissions.All;

            (await BuildWebHostAsync(args)).Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task<IWebHost> BuildWebHostAsync(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args)
                .Build();

            await CheckAddSuperAdminAsync(host);
            return host;
        }

        private static async Task CheckAddSuperAdminAsync(IWebHost host)
        {
            IServiceProvider services = host.Services;

            await services.CreateSuperAdmin();
            // await services.CreateUserRole();
        }
    }
}

// dotnet ef dbcontext scaffold "server=localhost;port=3306;user=atr-user;password=noDEjizi72SuDuqino6Ihu47CevuYA;database=atr" MySql.Data.EntityFrameworkCore