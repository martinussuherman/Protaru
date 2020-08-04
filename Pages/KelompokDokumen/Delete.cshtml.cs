using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KelompokDokumen
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.KelompokDokumen KelompokDokumen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.KelompokDokumen = await _context.KelompokDokumen
                .Include(k => k.JenisAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.KelompokDokumen == null)
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

            this.KelompokDokumen = await _context.KelompokDokumen.FindAsync(id);

            if (this.KelompokDokumen != null)
            {
                _context.KelompokDokumen.Remove(this.KelompokDokumen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}