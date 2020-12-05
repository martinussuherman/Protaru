using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace Protaru.PageModels
{
    [AllowAnonymous]
    public class View : PageModel
    {
        public View(PomeloDbContext context)
        {
            _context = context;
            _rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public RtrDetail RtrDetail { get; set; } = new RtrDetail();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            RtrDetail.Rtr = await _context.Atr
                .RtrIncludeAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);
            RtrDetail.KelompokDokumenList = await _rtrUtilities.LoadKelompokDokumenDanDokumen(
                RtrDetail.Rtr.KodeJenisAtr);
            await _rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                RtrDetail.Rtr,
                RtrDetail.KelompokDokumenList);
            return Page();
        }

        private readonly PomeloDbContext _context;
        private readonly RtrUtilities _rtrUtilities;
    }
}