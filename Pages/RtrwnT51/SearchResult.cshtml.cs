using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrwnT51
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
                .ByJenis(JenisRtrEnum.RtrwnT51)
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
            return await this.RtrZeroFieldExport(_context, JenisRtrEnum.RtrwnT51, rtr);
        }

        private readonly MonevAtrDbContext _context;
    }
}