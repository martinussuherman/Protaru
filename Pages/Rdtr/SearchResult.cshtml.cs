using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Rdtr
{
    public class SearchResultModel : SearchResultPageModel
    {
        public SearchResultModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            FilterByJenis(rtr);

            Hasil = _context.Atr
                .ByJenisList(rtr)
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
            RegulationName = "Perda";
            IsDisplayRegulation = (rtr.Perda == 1);
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;

            return Page();
        }

        private void FilterByJenis(AtrSearch rtr)
        {
            rtr.JenisList.Add((int)JenisRtrEnum.RdtrT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RdtrT52);
        }

        private readonly PomeloDbContext _context;
    }
}