using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrPulau
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
                .ByPulau(rtr.Pulau)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByIsPerdaPerpres(rtr)
                .RtrPulauInclude()
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
            rtr.JenisList.Add((int)JenisRtrEnum.RtrPulauT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrPulauT52);
        }

        private readonly PomeloDbContext _context;
    }
}