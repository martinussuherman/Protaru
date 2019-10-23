using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.RtrwnT52
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
                .Where(a => ((a.KodeJenisAtr == (int) JenisRtrEnum.RtrwnT51 &&
                            a.StatusRevisi >= 2) ||
                        a.KodeJenisAtr == (int) JenisRtrEnum.RtrwnT52) &&
                    a.SudahDirevisi == 0)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        private readonly MonevAtrDbContext _context;
    }
}