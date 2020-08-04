using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwT52
{
    [Authorize(Permissions.RtrwT52.Create)]
    public class CreateFilterModel : PageModel
    {
        public CreateFilterModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .Where(a => ((a.KodeJenisAtr == (int)JenisRtrEnum.RtrwT51 &&
                            a.StatusRevisi >= 2) ||
                        a.KodeJenisAtr == (int)JenisRtrEnum.RtrwT52) &&
                    a.SudahDirevisi == 0)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        private readonly PomeloDbContext _context;
    }
}