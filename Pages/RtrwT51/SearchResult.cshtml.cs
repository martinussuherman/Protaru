using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwT51
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(
            IAuthorizationService authorizationService,
            PomeloDbContext context)
        {
            _authorizationService = authorizationService;
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        [ViewData]
        public bool IsCanCreate { get; set; }

        public bool IsCanEdit { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RtrwT51)
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
                Permissions.RtrwT51.Create).Result.Succeeded;
            IsCanEdit = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RtrwT51.Edit).Result.Succeeded;

            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrProvKabKotaExport(_context, JenisRtrEnum.RtrwT51, rtr);
        }

        public IActionResult OnGetByProgress([FromQuery] int stage, [FromQuery] int page = 1)
        {
            AtrSearch rtr = new AtrSearch();
            AddProgressByStage(rtr, stage);
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RtrwT51)
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
                    rtr.ProgressList.Add(8);
                    rtr.ProgressList.Add(9);
                    break;
                case 2:
                    rtr.ProgressList.Add(15);
                    break;
                case 3:
                    rtr.ProgressList.Add(16);
                    rtr.ProgressList.Add(17);
                    break;
                case 4:
                    rtr.ProgressList.Add(18);
                    break;
                default:
                    rtr.ProgressList.Add(0);
                    break;
            }
        }

        private readonly IAuthorizationService _authorizationService;
        private readonly PomeloDbContext _context;
    }
}