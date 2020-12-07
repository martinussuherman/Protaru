using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrKsn
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
            FilterByJenis(rtr);
            Hasil = _context.Atr
                .ByJenisList(rtr)
                .ByKawasan(rtr.Kawasan)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByIsPerdaPerpres(rtr)
                .RtrKsnInclude()
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

        private void FilterByJenis(AtrSearch rtr)
        {
            rtr.JenisList.Add((int)JenisRtrEnum.RtrKsnT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrKsnT52);
        }

        private readonly PomeloDbContext _context;
    }
}