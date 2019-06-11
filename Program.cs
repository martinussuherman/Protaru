using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MonevAtr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

// dotnet add package MySql.Data
// dotnet add package MySql.Data.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet ef dbcontext scaffold "server=localhost;port=3306;user=atr-user;password=noDEjizi72SuDuqino6Ihu47CevuYA;database=atr" MySql.Data.EntityFrameworkCore