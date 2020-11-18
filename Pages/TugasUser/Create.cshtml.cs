using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.TugasUser
{
    public class CreateModel : PageModel
    {
        public CreateModel(
            PomeloDbContext context,
            IdentityDbContext identity)
        {
            _context = context;
            _identity = identity;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Protaru.Models.TugasUser TugasUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["User"] = await selectListUtilities.Users(_identity);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["User"] = await selectListUtilities.Users(_identity);
                return Page();
            }

            _context.TugasUser.Add(TugasUser);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private readonly SelectListUtilities selectListUtilities;
        private readonly PomeloDbContext _context;
        private readonly IdentityDbContext _identity;
    }
}