using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Protaru.Helper;

namespace MonevAtr.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

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
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email
                };

                IdentityResult result = await _userManager.CreateAsync(
                    user,
                    Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToPage("./List");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}