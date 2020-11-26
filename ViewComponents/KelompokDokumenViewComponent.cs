using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class KelompokDokumenViewComponent : ViewComponent
    {
        public KelompokDokumenViewComponent(PomeloDbContext context)
        {
            _rtrUtilities = new RtrUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(Atr rtr)
        {
            List<KelompokDokumen> list = await _rtrUtilities
                .LoadKelompokDokumenDanDokumen(rtr.KodeJenisAtr);
            await _rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(rtr, list);
            return View(new ViewModel
            {
                Rtr = rtr,
                KelompokDokumen = list
            });
        }

        public class ViewModel
        {
            public Atr Rtr { get; set; }

            public List<KelompokDokumen> KelompokDokumen { get; set; }
        }

        private readonly RtrUtilities _rtrUtilities;
    }
}