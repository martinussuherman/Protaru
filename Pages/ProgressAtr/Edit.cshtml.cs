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
        public EditModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.ProgressAtr ProgressRtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgressRtr = await _context.ProgressAtr
                .Include(p => p.JenisAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (ProgressRtr == null)
            {
                return NotFound();
            }

            ViewData["JenisRtr"] = await selectListUtilities.JenisRtr();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProgressRtr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressAtrExists(ProgressRtr.Kode))
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

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}