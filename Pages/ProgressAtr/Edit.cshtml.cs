using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class EditModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public EditModel(MonevAtr.Models.MonevAtrDbContext context)
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

            ViewData["KodeJenisAtr"] = _context.SelectListJenisAtr;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProgressAtr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressAtrExists(ProgressAtr.Kode))
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
    }
}
