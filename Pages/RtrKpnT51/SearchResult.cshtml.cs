using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKpnT51
{
    public class SearchResultModel : SearchResultPageModel
    {
        public SearchResultModel(
            IAuthorizationService authorizationService,
            PomeloDbContext context)
        {
            _authorizationService = authorizationService;
            _context = context;
        }

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
            Rtr = rtr;
            RegulationName = "Perpres";
            IsDisplayRegulation = true;
            IsUseCreateForm = false;
            IsCanCreate = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RdtrKpnT51.Create).Result.Succeeded;
            IsCanEdit = _authorizationService.AuthorizeAsync(
                User,
                Permissions.RdtrKpnT51.Edit).Result.Succeeded;

            return Page();
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            return await this.RtrProvKabKotaExport(_context, JenisRtrEnum.RtrKpnT51, rtr);
        }

        private readonly IAuthorizationService _authorizationService;
        private readonly PomeloDbContext _context;
    }
}