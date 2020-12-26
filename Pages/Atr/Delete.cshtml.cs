using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Atr
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public DeleteModel(PomeloDbContext context)
        {
            _context = context;
        }

        public RtrDetail RtrDetail { get; set; } = new RtrDetail();

        public string IndexPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RtrDetail.Rtr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.Provinsi)
                .Include(a => a.ProgressAtr)
                .Include(a => a.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            IndexPage = RetrieveIndexPage(RtrDetail.Rtr.KodeJenisAtr);

            if (RtrDetail.Rtr == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RtrDetail.Rtr = await _context.Atr.FindAsync(id);

            int? kodeJenisRtr = 0;

            if (RtrDetail.Rtr == null)
            {
                return RedirectToPage(RetrieveIndexPage(kodeJenisRtr));
            }

            List<AtrDokumen> dokumenList = await _context.AtrDokumen
                .Where(d => d.KodeAtr == id)
                .ToListAsync();

            List<RtrFasilitasKegiatan> fasilitasList =
                await _context.RtrFasilitasKegiatan
                .Where(f => f.KodeRtr == id)
                .ToListAsync();

            kodeJenisRtr = RtrDetail.Rtr.KodeJenisAtr;
            _context.AtrDokumen.RemoveRange(dokumenList);
            _context.RtrFasilitasKegiatan.RemoveRange(fasilitasList);
            _context.Atr.Remove(RtrDetail.Rtr);
            await _context.SaveChangesAsync();

            return RedirectToPage(RetrieveIndexPage(kodeJenisRtr));
        }

        private string RetrieveIndexPage(int? kodeJenisRtr)
        {
            switch (kodeJenisRtr)
            {
                case (int)JenisRtrEnum.RdtrT51:
                    return "/RdtrT51/Index";

                case (int)JenisRtrEnum.RdtrT52:
                    return "/RdtrT52/Index";

                case (int)JenisRtrEnum.RtrwT50:
                    return "/RtrwT50/Index";

                case (int)JenisRtrEnum.RtrwT51:
                    return "/RtrwT51/Index";

                case (int)JenisRtrEnum.RtrwT52:
                    return "/RtrwT52/Index";

                case (int)JenisRtrEnum.RtrPulauT51:
                    return "/RtrPulauT51/Index";

                case (int)JenisRtrEnum.RtrPulauT52:
                    return "/RtrPulauT52/Index";

                default:
                    break;
            }

            return "/Index";
        }

        private readonly PomeloDbContext _context;
    }
}