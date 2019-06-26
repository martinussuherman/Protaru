using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KabupatenKota
{
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.KabupatenKota KabupatenKota { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.KabupatenKota = await _context.KabupatenKota
                .Include(k => k.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.KabupatenKota == null)
            {
                return NotFound();
            }

            ViewData["KodeProvinsi"] = await _context.GetSelectListProvinsi();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(this.KabupatenKota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KabupatenKotaExists(this.KabupatenKota.Kode))
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

        private bool KabupatenKotaExists(int id)
        {
            return _context.KabupatenKota.Any(e => e.Kode == id);
        }

        private readonly MonevAtrDbContext _context;
    }
}