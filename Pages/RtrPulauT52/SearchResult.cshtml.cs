using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrPulauT52
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Rtr = rtr;
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RtrPulauT52)
                .ByPulau(rtr.Pulau)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrPulauInclude()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        private readonly MonevAtrDbContext _context;
    }
}