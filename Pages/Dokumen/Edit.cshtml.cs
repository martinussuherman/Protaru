using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
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

            ViewData["KelompokDokumen"] = await _context.GetSelectListKelompokDokumen();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(this.Dokumen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DokumenExists(this.Dokumen.Kode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DokumenExists(int id)
        {
            return _context.Dokumen.Any(e => e.Kode == id);
        }

        private readonly MonevAtrDbContext _context;
    }
}