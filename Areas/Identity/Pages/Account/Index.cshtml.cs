using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Itm.Identity;
using Itm.Misc;
using Protaru.Identity;

namespace MonevAtr.Areas.Identity.Pages.Account
{
    [Authorize(Permissions.Users.List)]
    public class IndexModel : CustomPageModel
    {
        public IndexModel(IdentityDbContext context)
        {
            _context = context;
            Title = "Daftar User";
            PageTitle = "User";
        }

        public class UserModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string RoleName { get; set; }
        }

        public List<UserModel> UserList { get; set; } = new List<UserModel>();

        public async Task<IActionResult> OnGet()
        {
            List<ApplicationUser> userList = await _context.Users
                .AsNoTracking()
                .ToListAsync();
            List<ApplicationRole> roleList = await _context.Roles
                .AsNoTracking()
                .ToListAsync();

            foreach (ApplicationUser user in userList)
            {
                UserList.Add(await Convert(user, roleList));
            }

            return Page();
        }

        private async Task<UserModel> Convert(
            ApplicationUser user,
            List<ApplicationRole> roleList)
        {
            IdentityUserRole<int> role = await _context.UserRoles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            UserModel result = new UserModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                RoleName = role == null ? String.Empty : roleList.First(r => r.Id == role.RoleId).Name
            };

            return result;
        }

        private readonly IdentityDbContext _context;
    }
}