using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwT50
{
    [Authorize(Permissions.RtrwT50.Edit)]
    public class EditModel : PageModel
    {
        public EditModel(PomeloDbContext context)
        {
            _context = context;
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Rtr { get; set; }

        [BindProperty]
        public List<AtrDokumen> Dokumen { get; set; }

        [BindProperty]
        public List<RtrFasilitasKegiatan> FasKeg { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public List<FasilitasKegiatan> FasilitasList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RtrwT50);
            FasilitasList = await rtrUtilities.LoadFasilitasKegiatan();
            Rtr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);
            await rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Rtr,
                id,
                KelompokDokumenList);
            await rtrUtilities.MergeRtrFasilitasKegiatan(Rtr, id, FasilitasList);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Models.Dokumen> dokumenList = await _context.Dokumen
                .ToListAsync();

            foreach (AtrDokumen dokumen in Dokumen)
            {
                if (!await rtrUtilities.SaveRtrDokumen(Rtr, dokumen, dokumenList))
                {
                    return NotFound();
                }
            }

            foreach (RtrFasilitasKegiatan fasilitas in FasKeg)
            {
                if (!await rtrUtilities.SaveRtrFasilitasKegiatan(Rtr, fasilitas))
                {
                    return NotFound();
                }
            }

            if (!await rtrUtilities.SaveRtr(Rtr, User, EntityState.Modified))
            {
                return NotFound();
            }

            return await OnGetAsync(Rtr.Kode);
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly PomeloDbContext _context;
    }
}