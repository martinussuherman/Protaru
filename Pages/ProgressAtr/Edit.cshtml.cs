using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
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

            ViewData["JenisAtr"] = await _context.GetSelectListJenisAtr();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(this.ProgressAtr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressAtrExists(this.ProgressAtr.Kode))
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

        private bool ProgressAtrExists(int id)
        {
            return _context.ProgressAtr.Any(e => e.Kode == id);
        }

        private readonly MonevAtrDbContext _context;
    }
}