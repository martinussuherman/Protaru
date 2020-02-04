using System;
using System.IO;
using Itm.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace MonevAtr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            _ = services.AddDbContextPool<Models.MonevAtrDbContext>(options =>
            {
                _ = options.UseMySQL(Configuration.GetConnectionString("MonevAtr"));
            });

            _ = services.AddDbContextPool<Models.PomeloDbContext>(options =>
            {
                _ = options.UseMySql(Configuration.GetConnectionString("MonevAtr"));
            });

            _ = services.AddDistributedMemoryCache();

            _ = services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            _ = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            _ = services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ProtaruViewLocationExpander());
            });

            // _ = services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            services.AddDbContextPool<IdentityDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("IdentityConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            10,
                            TimeSpan.FromSeconds(30),
                            null);
                    }), 16);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services.AddScoped<IAuthorizationHandler, Itm.Identity.PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, Itm.Identity.PermissionPolicyProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }
            else
            {
                _ = app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                _ = app.UseHsts();
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseStaticFiles();
            _ = app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "upload")),
                RequestPath = new PathString("/upload")
            });

            app
                .UsePathBase(Configuration.GetValue<string>("BasePath"))
                .UseSession()
                .UseAuthentication()
                .UseCookiePolicy()
                .UseMvc();

            PagerUrlHelper.ItemPerPage = 200;

            UploadFolderCreator folderCreator = new UploadFolderCreator(env);
            folderCreator.CreateUploadFolders();
        }
    }
}