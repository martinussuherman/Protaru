using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itm.Identity
{
    public static class SuperAdminSetup
    {
        // TODO : move to PermissionGlobalSetting
        private const string SuperAdminRoleName = "SuperAdmin";

        public static async Task CreateSuperAdmin(this IServiceProvider serviceProvider)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<ApplicationRole> roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                if (!await roleManager.RoleExistsAsync(SuperAdminRoleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole(SuperAdminRoleName));
                }

                ApplicationRole adminRole = await roleManager.FindByNameAsync(SuperAdminRoleName);
                IList<Claim> claimList = await roleManager.GetClaimsAsync(adminRole);

                if (!claimList
                    .Where(x => x.Type == PermissionGlobalSetting.CustomClaimType &&
                        x.Value == PermissionGlobalSetting.SuperPermission)
                    .Any())
                {
                    await roleManager.AddClaimAsync(adminRole,
                        new Claim(
                            PermissionGlobalSetting.CustomClaimType,
                            PermissionGlobalSetting.SuperPermission));
                }

                IConfigurationSection superSection = services
                    .GetRequiredService<IConfiguration>()
                    .GetSection(SuperAdminRoleName);

                if (superSection == null)
                    return;

                string userName = superSection["Username"];
                string userEmail = superSection["Email"];
                string userPassword = superSection["Password"];

                ApplicationUser user = await userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    user = new ApplicationUser(userName);
                    await userManager.CreateAsync(user);
                    await userManager.SetUserNameAsync(user, userName);
                    await userManager.SetEmailAsync(user, userEmail);
                    await userManager.AddPasswordAsync(user, userPassword);
                    await userManager.AddToRoleAsync(user, SuperAdminRoleName);
                }
            }
        }
    }
}