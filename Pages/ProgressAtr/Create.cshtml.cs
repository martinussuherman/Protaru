using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.ProgressAtr ProgressRtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["JenisRtr"] = await selectListUtilities.JenisRtr();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProgressAtr.Add(ProgressRtr);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}