using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.KelompokDokumen
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
            ViewData["KodeJenisAtr"] = _context.SelectListJenisAtr;
            return Page();
        }

        [BindProperty]
        public MonevAtr.Models.KelompokDokumen KelompokDokumen { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.KelompokDokumen.Add(KelompokDokumen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}