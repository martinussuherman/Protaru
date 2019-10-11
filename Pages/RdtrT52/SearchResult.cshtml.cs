using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RdtrT52
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
                .ByJenis(JenisRtrEnum.RdtrT52)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }
        public IActionResult OnGetByProgress([FromQuery] int stage, [FromQuery] int page = 1)
        {
            AtrSearch rtr = new AtrSearch();
            Rtr = rtr;
            AddProgressByStage(rtr, stage);
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RdtrT52)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        private void AddProgressByStage(AtrSearch rtr, int stage)
        {
            switch (stage)
            {
                case 1:
                    rtr.ProgressList.Add(38);
                    rtr.ProgressList.Add(39);
                    rtr.ProgressList.Add(40);
                    rtr.ProgressList.Add(41);
                    rtr.ProgressList.Add(42);
                    break;
                case 2:
                    rtr.ProgressList.Add(44);
                    rtr.ProgressList.Add(45);
                    break;
                case 3:
                    rtr.ProgressList.Add(46);
                    break;
                case 4:
                    rtr.ProgressList.Add(47);
                    rtr.ProgressList.Add(48);
                    break;
                case 5:
                    rtr.ProgressList.Add(49);
                    break;
                default:
                    rtr.ProgressList.Add(0);
                    break;
            }
        }

        private readonly MonevAtrDbContext _context;
    }
}