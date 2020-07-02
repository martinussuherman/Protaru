using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RdtrT52
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(
            IAuthorizationService authorizationService,
            MonevAtrDbContext context)
        {
            _authorizationService = authorizationService;
            _context = context;
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IPager<Models.Atr> Hasil { get; set; }

        [ViewData]
        public bool IsCanCreate { get; set; }

        public bool IsCanEdit { get; set; }

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
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);

            IsCanCreate = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RdtrT52.Create).Result.Succeeded;
            IsCanEdit = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RdtrT52.Edit).Result.Succeeded;

            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrProvKabKotaExport(_context, JenisRtrEnum.RdtrT52, rtr);
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
                .AsNoTracking()
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

        private readonly IAuthorizationService _authorizationService;
        private readonly MonevAtrDbContext _context;
    }
}