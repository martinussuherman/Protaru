using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrPulauT52
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
                .ByJenis(JenisRtrEnum.RtrPulauT52)
                .ByPulau(rtr.Pulau)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrPulauInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);

            IsCanCreate = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RtrPulauT52.Create).Result.Succeeded;
            IsCanEdit = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RtrPulauT52.Edit).Result.Succeeded;

            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrOneFieldExport(_context, JenisRtrEnum.RtrPulauT52, rtr);
        }

        private readonly IAuthorizationService _authorizationService;
        private readonly PomeloDbContext _context;
    }
}