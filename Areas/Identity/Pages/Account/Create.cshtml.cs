using System.ComponentModel.DataAnnotations;
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
    public class CreateModel : CustomPageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CreateModel> _logger;
        private readonly IdentityDbContext _identityContext;
        private readonly SelectListUtilities selectListUtilities;

        public CreateModel(
            UserManager<ApplicationUser> userManager,
            ILogger<CreateModel> logger,
            PomeloDbContext context,
            IdentityDbContext identityContext)
        {
            _userManager = userManager;
            _logger = logger;
            _identityContext = identityContext;
            selectListUtilities = new SelectListUtilities(context);
            Title = "Tambah User";
            PageTitle = "User";
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = ViewMessage.UsernameRequired)]
            public string UserName { get; set; }

            [Required(ErrorMessage = ViewMessage.EmailRequired)]
            [EmailAddress(ErrorMessage = ViewMessage.MalformedEmail)]
            public string Email { get; set; }

            [Required(ErrorMessage = ViewMessage.PasswordRequired)]
            [StringLength(100, ErrorMessage = ViewMessage.PasswordLength, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = ViewMessage.ConfirmPasswordNotMatch)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = ViewMessage.RoleRequired)]
            public string UserRole { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["UserRole"] = await selectListUtilities.UserRoles(_identityContext);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);

                if (!LogSuccessAndError(result, "User created a new account with password."))
                {
                    return await OnGetAsync();
                }

                result = await _userManager.AddToRoleAsync(user, Input.UserRole);

                if (!LogSuccessAndError(result, "User added to role."))
                {
                    return await OnGetAsync();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LogSuccessAndError(IdentityResult result, string logMessage)
        {
            if (result.Succeeded)
            {
                _logger.LogInformation(logMessage);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return result.Succeeded;
        }
    }
}