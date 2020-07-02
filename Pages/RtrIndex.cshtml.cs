using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Itm.Misc;

namespace MonevAtr.Pages
{
    public class RtrIndexModel : CustomPageModel
    {
        public RtrIndexModel(MonevAtrDbContext context)
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
