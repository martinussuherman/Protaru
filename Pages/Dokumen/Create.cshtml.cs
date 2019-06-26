using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["KelompokDokumen"] = await _context.GetSelectListKelompokDokumen();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dokumen.Add(this.Dokumen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}