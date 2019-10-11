using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages_Dokumen
{
    public class DeleteModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DeleteModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen).FirstOrDefaultAsync(m => m.Kode == id);

            if (Dokumen == null)
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

            Dokumen = await _context.Dokumen.FindAsync(id);

            if (Dokumen != null)
            {
                _context.Dokumen.Remove(Dokumen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
