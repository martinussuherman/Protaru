using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Itm.Identity
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>, IAuthorizationHandler
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<ApplicationRole> _roleManager;

        public PermissionAuthorizationHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return;
            }

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            ApplicationUser user = await _userManager.GetUserAsync(context.User);
            IList<Claim> claimList = await _userManager.GetClaimsAsync(user);

            if (claimList
                .Where(c => c.Type == PermissionGlobalSetting.CustomClaimType &&
                    c.Value == PermissionGlobalSetting.SuperPermission &&
                    c.Issuer == "LOCAL AUTHORITY")
                .Select(c => c.Value)
                .Any())
            {
                context.Succeed(requirement);
                return;
            }

            if (claimList
                .Where(c => c.Type == PermissionGlobalSetting.CustomClaimType &&
                    c.Value == requirement.PermissionName &&
                    c.Issuer == "LOCAL AUTHORITY")
                .Select(c => c.Value)
                .Any())
            {
                context.Succeed(requirement);
                return;
            }

            IList<string> userRoleNames = await _userManager.GetRolesAsync(user);
            List<ApplicationRole> userRoles = _roleManager.Roles
                .Where(x => userRoleNames.Contains(x.Name))
                .ToList();

            foreach (ApplicationRole role in userRoles)
            {
                IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);

                if (roleClaims
                    .Where(x => x.Type == PermissionGlobalSetting.CustomClaimType &&
                        x.Value == PermissionGlobalSetting.SuperPermission)
                    .Select(x => x.Value)
                    .Any())
                {
                    context.Succeed(requirement);
                    return;
                }

                IEnumerable<string> permissions = roleClaims
                    .Where(x => x.Type == PermissionGlobalSetting.CustomClaimType &&
                        x.Value == requirement.PermissionName)
                    .Select(x => x.Value);

                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}