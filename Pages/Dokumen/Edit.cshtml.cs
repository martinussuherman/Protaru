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
        public EditModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (Dokumen == null)
            {
                return NotFound();
            }

            ViewData["KodeKelompokDokumen"] = await selectListUtilities.KelompokDokumen();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Dokumen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DokumenExists(Dokumen.Kode))
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

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}