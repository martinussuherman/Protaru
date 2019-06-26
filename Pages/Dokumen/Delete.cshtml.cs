using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.Dokumen == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.Dokumen = await _context.Dokumen.FindAsync(id);

            if (this.Dokumen != null)
            {
                _context.Dokumen.Remove(this.Dokumen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}