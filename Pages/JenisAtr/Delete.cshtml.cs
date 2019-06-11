using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class DeleteModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DeleteModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MonevAtr.Models.JenisAtr JenisAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JenisAtr = await _context.JenisAtr.FirstOrDefaultAsync(m => m.Kode == id);

            if (JenisAtr == null)
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

            JenisAtr = await _context.JenisAtr.FindAsync(id);

            if (JenisAtr != null)
            {
                _context.JenisAtr.Remove(JenisAtr);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
