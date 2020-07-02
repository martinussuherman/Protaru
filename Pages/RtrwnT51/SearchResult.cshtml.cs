using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwnT51
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

        public IPager<Models.Atr> Hasil { get; set; }

        [ViewData]
        public bool IsCanCreate { get; set; }

        public bool IsCanEdit { get; set; }

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

            IsCanCreate = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RtrwnT51.Create).Result.Succeeded;
            IsCanEdit = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RtrwnT51.Edit).Result.Succeeded;

            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrZeroFieldExport(_context, JenisRtrEnum.RtrwnT51, rtr);
        }

        private readonly IAuthorizationService _authorizationService;
        private readonly MonevAtrDbContext _context;
    }
}