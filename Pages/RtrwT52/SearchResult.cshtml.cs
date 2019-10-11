using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrwT52
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
                .ByJenis(JenisRtrEnum.RtrwT52)
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
                .ByJenis(JenisRtrEnum.RtrwT52)
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
                    rtr.ProgressList.Add(20);
                    rtr.ProgressList.Add(21);
                    rtr.ProgressList.Add(22);
                    rtr.ProgressList.Add(23);
                    rtr.ProgressList.Add(24);
                    break;
                case 2:
                    rtr.ProgressList.Add(25);
                    rtr.ProgressList.Add(26);
                    break;
                case 3:
                    rtr.ProgressList.Add(27);
                    break;
                case 4:
                    rtr.ProgressList.Add(28);
                    rtr.ProgressList.Add(29);
                    break;
                case 5:
                    rtr.ProgressList.Add(30);
                    break;
                default:
                    rtr.ProgressList.Add(0);
                    break;
            }
        }

        private readonly MonevAtrDbContext _context;
    }
}