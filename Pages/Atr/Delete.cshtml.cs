using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Atr
{
    public class DeleteModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DeleteModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MonevAtr.Models.Atr Atr { get; set; }

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
                .Include(a => a.Provinsi).FirstOrDefaultAsync(m => m.Kode == id);

            if (Atr == null)
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

            Atr = await _context.Atr.FindAsync(id);

            if (Atr != null)
            {
                _context.Atr.Remove(Atr);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
