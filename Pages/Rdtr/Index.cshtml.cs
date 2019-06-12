using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rdtr
{
    public class IndexModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public IndexModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Provinsi"] = _context.SelectListProvinsi;
            ViewData["KabupatenKota"] = _context.EmptySelectListKabupatenKota;
            return Page();
        }

        [BindProperty]
        public MonevAtr.Models.Atr Atr { get; set; }

        public IList<Models.ProgressAtr> ProgressAtr
        {
            get
            {
                return (from progressAtr in _context.ProgressAtr
                        where progressAtr.KodeJenisAtr == 1
                        select progressAtr).ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // _context.KabupatenKota.Add(KabupatenKota);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}