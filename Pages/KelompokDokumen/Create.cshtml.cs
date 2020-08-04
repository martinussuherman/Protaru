using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.KelompokDokumen
{
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.KelompokDokumen KelompokDokumen { get; set; }

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

            _context.KelompokDokumen.Add(KelompokDokumen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}