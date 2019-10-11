using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public int StatusYear { get; set; }

        public int StatusMonth { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusYear = DateTime.Today.Year;
            StatusMonth = DateTime.Today.Month;
            ViewData["TahunLintasSektorDanPersetujuanSubstansi"] =
                await selectListUtilities.TahunRapatLintasSektorDanPersetujuanSubstansi();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}