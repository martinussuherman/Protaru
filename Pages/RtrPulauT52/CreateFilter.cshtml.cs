using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrPulauT52
{
    [Authorize]
    public class CreateFilterModel : PageModel
    {
        public CreateFilterModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .Where(a => (a.KodeJenisAtr == (int) JenisRtrEnum.RtrPulauT51 ||
                        a.KodeJenisAtr == (int) JenisRtrEnum.RtrPulauT52) &&
                    a.SudahDirevisi == 0)
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