using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RdtrT52
{
    [Authorize(Permissions.RdtrT52.Edit)]
    public class EditModel : PageModel
    {
        public EditModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
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
                (int)JenisRtrEnum.RdtrT52);
            FasilitasList = await rtrUtilities.LoadFasilitasKegiatan();

            Rtr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            await rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Rtr,
                id,
                KelompokDokumenList);
            await rtrUtilities.MergeRtrFasilitasKegiatan(Rtr, id, FasilitasList);
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrRevisi;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Rtr.Kode);
            // }

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
        private readonly SelectListUtilities selectListUtilities;
        private readonly PomeloDbContext _context;
    }
}