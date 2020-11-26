using System;
using System.IO;
using Itm.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using Protaru.Helper;

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
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();
            services.AddHttpClient(Options.DefaultName);
            services.AddMvc();
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });

            services
                .Configure<SmtpOptions>(
                    Configuration.GetSection(SmtpOptions.OptionsName))
                .AddDbContextPool<Models.PomeloDbContext>(options =>
                    options.UseMySql(
                        Configuration.GetConnectionString("MonevAtr"),
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                10,
                                TimeSpan.FromSeconds(30),
                                null);
                        }),
                    16)
                .AddDbContextPool<IdentityDbContext>(options =>
                    options.UseMySql(
                        Configuration.GetConnectionString("IdentityConnection"),
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                10,
                                TimeSpan.FromSeconds(30),
                                null);
                        }),
                    16)
                .AddTransient<IEmailSender, SmtpEmailSender>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
                .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    // options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                })
                .Configure<RazorViewEngineOptions>(options =>
                {
                    options.ViewLocationExpanders.Add(new ProtaruViewLocationExpander());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                .UsePathBase(Configuration.GetValue<string>("BasePath"))
                .UseRouting()
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllers();
                })
                .UseStaticFiles()
                .UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.WebRootPath, "upload")),
                    RequestPath = new PathString("/upload")
                });

            PagerUrlHelper.ItemPerPage = 200;

            UploadFolderCreator folderCreator = new UploadFolderCreator(env);
            folderCreator.CreateUploadFolders();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                Configuration.GetValue<string>("SfKey"));
        }
    }
}