using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrKpn
{
    public class SearchResultModel : SearchResultPageModel
    {
        public SearchResultModel(PomeloDbContext context)
        {
            _context = context;
        }

        public string ReturnPage { get; set; }

        public IActionResult OnGet(
            [FromQuery] AtrSearch rtr,
            [FromQuery] string returnPage,
            [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RtrKpn)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByIsPerdaPerpres(rtr)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            Rtr = rtr;
            RegulationName = "Perpres";
            IsDisplayRegulation = rtr.Perda == 1;
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;
            ReturnPage = returnPage;

            return Page();
        }

        private readonly PomeloDbContext _context;
    }
}