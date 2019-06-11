using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class EditModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public EditModel(MonevAtr.Models.MonevAtrDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JenisAtr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JenisAtrExists(JenisAtr.Kode))
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
    }
}
