using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Provinsi
{
    public class DeleteModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DeleteModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MonevAtr.Models.Provinsi Provinsi { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provinsi = await _context.Provinsi.FirstOrDefaultAsync(m => m.Kode == id);

            if (Provinsi == null)
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

            Provinsi = await _context.Provinsi.FindAsync(id);

            if (Provinsi != null)
            {
                _context.Provinsi.Remove(Provinsi);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
