using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Atr
{
    public class CreateModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public CreateModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["KodeJenisAtr"] = new SelectList(_context.JenisAtr, "Kode", "Nama");
            ViewData["KodeProgressAtr"] = new SelectList(_context.ProgressAtr, "Kode", "Nama");
            ViewData["KodeKabupatenKota"] = _context.EmptySelectListKabupatenKota;
            ViewData["KodeProvinsi"] = _context.SelectListProvinsi;

            return Page();
        }

        [BindProperty]
        public MonevAtr.Models.Atr Atr { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Atr.Add(Atr);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IEnumerable<SelectListItem> KabupatenKotaBerdasarkanProvinsi(int kodeProvinsi)
        {
            using (_context)
            {
                IEnumerable<SelectListItem> regions = _context.KabupatenKota.AsEnumerable()
                    .OrderBy(n => n.Nama)
                    .Where(n => n.KodeProvinsi == kodeProvinsi)
                    .Select(n =>
                       new SelectListItem
                       {
                           Value = n.Kode.ToString(),
                           Text = n.Nama
                       }).ToList();
                return new SelectList(regions, "Value", "Text");
            }
        }
    }
}