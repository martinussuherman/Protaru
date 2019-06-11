using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class DeleteModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DeleteModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MonevAtr.Models.ProgressAtr ProgressAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgressAtr = await _context.ProgressAtr
                .Include(p => p.JenisAtr).FirstOrDefaultAsync(m => m.Kode == id);

            if (ProgressAtr == null)
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

            ProgressAtr = await _context.ProgressAtr.FindAsync(id);

            if (ProgressAtr != null)
            {
                _context.ProgressAtr.Remove(ProgressAtr);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
