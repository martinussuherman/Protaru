using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Areas.Page;

namespace MonevAtr.Pages
{
    public class RtrIndexModel : ProtaruPageModel
    {
        public RtrIndexModel(MonevAtrDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public int StatusYear { get; set; }

        public int StatusMonth { get; set; }

        public async Task<IActionResult> OnGetAsync(int? rtrType)
        {
            if (rtrType == 1)
            {
                Layout = "_RtrNasionalLayout";
            }

            if (rtrType == 2)
            {
                Layout = "_RtrDaerahLayout";
            }

            Title = "Kementerian Agraria dan Tata Ruang";
            ActiveMenu = ActiveMenu.Home;
            StatusYear = DateTime.Today.Year;
            StatusMonth = DateTime.Today.Month;
            ViewData["TahunLintasSektorDanPersetujuanSubstansi"] =
                await selectListUtilities.TahunRapatLintasSektorDanPersetujuanSubstansi();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}