using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MonevAtr.Areas.Identity
{
    public class IdentitySeed
    {
        private readonly RoleManager<IdentityRole> _rolesManager;
        private readonly ILogger _logger;

        public IdentitySeed(
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            _rolesManager = roleManager;
            _logger = loggerFactory.CreateLogger<IdentitySeed>();
        }

        public async Task CreateRoles()
        {
            string[] roleNames = new string[] { "Admin", "User" };

            foreach (string roleName in roleNames)
            {
                bool exists = await _rolesManager.RoleExistsAsync(roleName);

                if (!exists)
                {
                    IdentityResult result = await _rolesManager.CreateAsync(new IdentityRole { Name = roleName });
                    _logger.LogInformation("Create {0}: {1}", roleName, result.Succeeded);
                }
            }
        }
    }
}