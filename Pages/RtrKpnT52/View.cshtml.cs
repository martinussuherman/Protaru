using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrKpnT52
{
    [AllowAnonymous]
    public class ViewModel : PageModel
    {
        public ViewModel(PomeloDbContext context)
        {
            _context = context;
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public RtrDetail RtrDetail { get; set; } = new RtrDetail();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            RtrDetail.KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RtrKpnT52);
            RtrDetail.Rtr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            await rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                RtrDetail.Rtr,
                id,
                RtrDetail.KelompokDokumenList);
            ViewData["StatusRevisi"] = StatusRevisi.NamaStatusRevisiRevisi(
                RtrDetail.Rtr.StatusRevisi);
            return Page();
        }

        private readonly RtrUtilities rtrUtilities;

        private readonly PomeloDbContext _context;
    }
}