using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrKpnT51
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RtrKpnT51)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrProvKabKotaExport(_context, JenisRtrEnum.RtrKpnT51, rtr);
        }

        private readonly MonevAtrDbContext _context;
    }
}