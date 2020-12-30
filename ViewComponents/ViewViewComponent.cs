using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class ViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RtrDetail detail)
        {
            var jenis = (JenisRtrEnum)detail.Rtr.KodeJenisAtr;
            return View(new ViewModel
            {
                Rtr = detail,
                PreviousPage = PreviousPageName(jenis),
                NextPage = NextPageName(jenis)
            });
        }

        public class ViewModel
        {
            public RtrDetail Rtr { get; set; }
            public string PreviousPage { get; set; }
            public string NextPage { get; set; }
        }

        private string PreviousPageName(JenisRtrEnum jenis)
        {
            return ((JenisRtrEnum)((int)jenis - 1)).ToString();
        }
        private string NextPageName(JenisRtrEnum jenis)
        {
            return ((JenisRtrEnum)((int)jenis + 1)).ToString();
        }
    }
}