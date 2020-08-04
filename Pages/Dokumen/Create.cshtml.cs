using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["KodeKelompokDokumen"] = await selectListUtilities.KelompokDokumen();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dokumen.Add(Dokumen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}