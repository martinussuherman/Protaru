using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ListModel : PageModel
    {
        public ListModel(Data.MonevAtrIdentityDbContext context)
        {
            _context = context;
        }

        public class UserModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string RoleName { get; set; }
        }

        public List<UserModel> UserList { get; set; } = new List<UserModel>();

        public async Task<IActionResult> OnGet()
        {
            List<IdentityUser> userList = await _context.Users
                .ToListAsync();
            List<IdentityRole> roleList = await _context.Roles
                .ToListAsync();

            foreach (IdentityUser user in userList)
            {
                UserList.Add(await Convert(user, roleList));
            }

            return Page();
        }

        private async Task<UserModel> Convert(
            IdentityUser user,
            List<IdentityRole> roleList)
        {
            IdentityUserRole<string> role = await _context.UserRoles
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            UserModel result = new UserModel
            {
                Id = user.Id,
                Name = user.UserName,
                RoleName = role == null ? String.Empty : roleList.First(r => r.Id == role.RoleId).Name
            };

            return result;
        }

        private readonly Data.MonevAtrIdentityDbContext _context;
    }
}