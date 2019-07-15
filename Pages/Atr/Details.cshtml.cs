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
    public class DetailsModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DetailsModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

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
                .Include(a => a.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (Atr == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}