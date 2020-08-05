using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Rtrw
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        [ViewData]
        public bool IsCanCreate { get; set; }

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

            IsCanCreate = false;

            return Page();
        }

        private void FilterByJenis(AtrSearch rtr)
        {
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT50);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT52);
        }

        private readonly PomeloDbContext _context;
    }
}