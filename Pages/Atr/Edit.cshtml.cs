using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Atr
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PomeloDbContext _context;

        public EditModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.KabupatenKota)
                .Include(a => a.ProgressAtr)
                .Include(a => a.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (Atr == null)
            {
                return NotFound();
            }
            ViewData["KodeJenisAtr"] = new SelectList(_context.JenisAtr, "Kode", "Kode");
            ViewData["KodeKabupatenKota"] = new SelectList(_context.KabupatenKota, "Kode", "Kode");
            ViewData["KodeProgressAtr"] = new SelectList(_context.ProgressAtr, "Kode", "Kode");
            ViewData["KodeProvinsi"] = new SelectList(_context.Provinsi, "Kode", "Kode");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Atr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtrExists(Atr.Kode))
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

        private bool AtrExists(int id)
        {
            return _context.Atr.Any(e => e.Kode == id);
        }
    }
}