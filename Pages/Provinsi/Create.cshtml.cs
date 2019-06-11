using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Provinsi
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
            return Page();
        }

        [BindProperty]
        public MonevAtr.Models.Provinsi Provinsi { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Provinsi.Add(Provinsi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}