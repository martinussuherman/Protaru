using Itm.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Protaru.Identity;
using System.Threading.Tasks;

namespace MonevAtr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            PermissionGlobalSetting.CustomClaimType = Permissions.CustomClaimTypes;
            PermissionGlobalSetting.SuperPermission = Permissions.All;

            (await BuildWebHostAsync(args)).Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging
                        .ClearProviders()
                        .AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task<IHost> BuildWebHostAsync(string[] args)
        {
            IHost host = CreateHostBuilder(args)
                .Build();

            await CheckAddSuperAdminAsync(host);
            return host;
        }

        private static async Task CheckAddSuperAdminAsync(IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            System.IServiceProvider services = scope.ServiceProvider;
            await services.CreateSuperAdmin();
            // await services.CreateUserRole();
        }
    }
}

// dotnet ef dbcontext scaffold "server=localhost;port=3306;user=atr-user;password=noDEjizi72SuDuqino6Ihu47CevuYA;database=atr" MySql.Data.EntityFrameworkCore