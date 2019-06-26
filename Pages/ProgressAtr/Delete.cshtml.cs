using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.ProgressAtr ProgressAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.ProgressAtr = await _context.ProgressAtr
                .Include(p => p.JenisAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.ProgressAtr == null)
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

            this.ProgressAtr = await _context.ProgressAtr.FindAsync(id);

            if (this.ProgressAtr != null)
            {
                _context.ProgressAtr.Remove(this.ProgressAtr);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}