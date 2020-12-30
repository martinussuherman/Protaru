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
                NextPage = NextPageName(jenis),
                Title = TitleByJenis(detail.Rtr, jenis),
                StatusNomor = StatusNomor(detail.Rtr)
            });
        }

        public class ViewModel
        {
            public RtrDetail Rtr { get; set; }
            public string PreviousPage { get; set; }
            public string NextPage { get; set; }
            public string Title { get; set; }
            public string StatusNomor { get; set; }
        }

        private string StatusNomor(Atr rtr)
        {
            if(string.IsNullOrEmpty(rtr.Nomor) )
            {
                return rtr.DisplayNamaProgress;
            }

            return $"{rtr.DisplayNamaProgress} Nomor: {rtr.Nomor}";
        }
        private string TitleByJenis(Atr rtr, JenisRtrEnum jenis)
        {
            switch (jenis)
            {
                case JenisRtrEnum.RtrwnT51:
                case JenisRtrEnum.RtrwnT52:
                    return string.Empty;

                case JenisRtrEnum.RtrPulauT51:
                case JenisRtrEnum.RtrPulauT52:
                    return rtr.DisplayNamaPulau;

                case JenisRtrEnum.RtrKsnT51:
                case JenisRtrEnum.RtrKsnT52:
                    return rtr.DisplayNamaKawasan;
            }

            return rtr.DisplayNamaProvinsi;
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