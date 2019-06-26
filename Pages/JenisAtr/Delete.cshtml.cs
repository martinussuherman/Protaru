using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(MonevAtrDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.JenisAtr = await _context.JenisAtr.FindAsync(id);

            if (this.JenisAtr != null)
            {
                _context.JenisAtr.Remove(this.JenisAtr);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}