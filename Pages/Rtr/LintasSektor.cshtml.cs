using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages
{
    public class LintasSektorModel : PageModel
    {
        public LintasSektorModel(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public int StatusYear { get; set; }

        public int StatusMonth { get; set; }

        public IEnumerable<SelectListItem> Tahun { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusYear = DateTime.Today.Year;
            StatusMonth = DateTime.Today.Month;
            Tahun = await _selectListUtilities.TahunRapatLinsekPersubAsync();
            return Page();
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}