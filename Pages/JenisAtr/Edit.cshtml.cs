using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.JenisAtr JenisAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.JenisAtr = await _context.JenisAtr
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.JenisAtr == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(this.JenisAtr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JenisAtrExists(this.JenisAtr.Kode))
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

        private bool JenisAtrExists(int id)
        {
            return _context.JenisAtr.Any(e => e.Kode == id);
        }

        private readonly MonevAtrDbContext _context;
    }
}