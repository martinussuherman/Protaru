using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
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
            ViewData["KodeKelompokDokumen"] = _context.SelectListKelompokDokumen;
            return Page();
        }

        [BindProperty]
        public MonevAtr.Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dokumen.Add(Dokumen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}