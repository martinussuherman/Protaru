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
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<Models.AtrDokumen> AtrDokumenList { get; set; }

        [BindProperty]
        public List<RtrFasilitasKegiatan> FasKeg { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public List<FasilitasKegiatan> FasilitasList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RtrwT50);
            FasilitasList = await rtrUtilities.LoadFasilitasKegiatan();

            Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);

            await rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Atr,
                id,
                KelompokDokumenList);
            await rtrUtilities.MergeRtrFasilitasKegiatan(Atr, id, FasilitasList);

            ViewData["Progress"] = await selectListUtilities.ProgressRtrwT50();
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrRegular;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Atr.Kode);
            // }

            dokumenList = await _context.Dokumen
                .ToListAsync();

            foreach (AtrDokumen dokumen in AtrDokumenList)
            {
                if (!await rtrUtilities.SaveRtrDokumen(Atr, dokumen, dokumenList))
                {
                    return NotFound();
                }
            }

            foreach (RtrFasilitasKegiatan fasilitas in FasKeg)
            {
                if (!await rtrUtilities.SaveRtrFasilitasKegiatan(Atr, fasilitas))
                {
                    return NotFound();
                }
            }

            if (!await rtrUtilities.SaveRtr(Atr, User))
            {
                return NotFound();
            }

            return await OnGetAsync(Atr.Kode);
        }

        private List<Models.Dokumen> dokumenList;

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}