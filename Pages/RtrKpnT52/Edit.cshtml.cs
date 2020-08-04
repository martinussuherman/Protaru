using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKpnT52
{
    [Authorize(Permissions.RdtrKpnT52.Edit)]
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
        public List<AtrDokumenTindakLanjut> DokTin { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RtrKpnT52);

            Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            await rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Atr,
                id,
                KelompokDokumenList);

            ViewData["Progress"] = await selectListUtilities.ProgressRtrKpnT52();
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrRevisi;
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

            foreach (Models.AtrDokumen dokumen in this.AtrDokumenList)
            {
                if (!await rtrUtilities.SaveRtrDokumen(this.Atr, dokumen, dokumenList))
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