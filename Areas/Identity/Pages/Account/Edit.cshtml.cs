using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Itm.Identity;
using Itm.Misc;
using Protaru.Identity;
using MonevAtr.Models;
using Protaru.Helper;

namespace MonevAtr.Areas.Identity.Pages.Account
{
    [Authorize(Permissions.Users.All)]
    public class EditModel : CustomPageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IdentityDbContext _identityContext;
        private readonly SelectListUtilities selectListUtilities;

        public EditModel(
            UserManager<ApplicationUser> userManager,
            ILogger<EditModel> logger,
            PomeloDbContext context,
            IdentityDbContext identityContext)
        {
            _userManager = userManager;
            _logger = logger;
            _identityContext = identityContext;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Ubah User";
            PageTitle = "User";
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [BindProperty]
        public List<int> Permission { get; set; }

        public List<PermissionGroupInfo> PermissionInfo { get; set; }

        public List<bool> SavedPermissionMap { get; set; }

        public class InputModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = ViewMessage.UsernameRequired)]
            public string UserName { get; set; }

            [Required(ErrorMessage = ViewMessage.EmailRequired)]
            [EmailAddress(ErrorMessage = ViewMessage.MalformedEmail)]
            public string Email { get; set; }

            [Required(ErrorMessage = ViewMessage.RoleRequired)]
            public string UserRole { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            Input.Id = user.Id;
            Input.UserName = user.UserName;
            Input.Email = user.Email;
            Input.UserRole = roles[0];
            ViewData["UserRole"] = await selectListUtilities.UserRoles(_identityContext);

            PermissionListFactory factory = new PermissionListFactory();
            factory.BuildViewList();
            factory.BuildLookupList();
            SavedPermissionMap = factory.BuildUserSavedPermissionList(await _userManager.GetClaimsAsync(user));
            PermissionInfo = PermissionListFactory.ViewList;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = new ApplicationUser();

            if (ModelState.IsValid)
            {
                user = await _userManager.FindByIdAsync(Input.Id.ToString());
            }

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.SetEmailAsync(user, Input.Email);
            IdentityResult result = await _userManager.RemoveFromRolesAsync(
                user,
                await _userManager.GetRolesAsync(user));

            if (!LogSuccessAndError(result, "Remove old roles."))
            {
                return NotFound();
            }

            result = await _userManager.AddToRoleAsync(user, Input.UserRole);

            if (!LogSuccessAndError(result, "Changed user role."))
            {
                return NotFound();
            }

            PermissionListFactory factory = new PermissionListFactory();
            factory.BuildViewList();
            factory.BuildLookupList();

            if (Permission == null)
            {
                return RedirectToPage("./Index");
            }

            result = await _userManager.RemoveClaimsAsync(
                user,
                await _userManager.GetClaimsAsync(user));

            if (!LogSuccessAndError(result, "Remove saved user claims."))
            {
                return NotFound();
            }

            List<Claim> claimList = new List<Claim>();

            foreach (int code in Permission)
            {
                PermissionInfo item = PermissionListFactory.LookupList.Find(e => e.Code == code);
                claimList.Add(new Claim(Permissions.CustomClaimTypes, item.Name));
            }

            result = await _userManager.AddClaimsAsync(user, claimList);

            if (!LogSuccessAndError(result, "Save user claims."))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool LogSuccessAndError(IdentityResult result, string logMessage)
        {
            if (result.Succeeded)
            {
                _logger.LogInformation(logMessage);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return result.Succeeded;
        }
    }
}