using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RdtrT51
{
    [Authorize]
    public class EditTrial : PageModel
    {
        public EditTrial(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public RtrDetail RtrDetail { get; set; } = new RtrDetail();

        [BindProperty]
        public List<AtrDokumen> DokumenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            RtrDetail.KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RdtrT51);
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
            ViewData["ProgressRdtr"] = await selectListUtilities.ProgressRdtrT51();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            dokumenList = await _context.Dokumen
                .ToListAsync();

            foreach (Models.AtrDokumen dokumen in DokumenList)
            {
                if (!await rtrUtilities.SaveRtrDokumen(RtrDetail.Rtr, dokumen, dokumenList))
                {
                    return NotFound();
                }
            }

            if (!await rtrUtilities.SaveRtr(RtrDetail.Rtr, User))
            {
                return NotFound();
            }

            return await OnGetAsync(RtrDetail.Rtr.Kode);
        }

        private List<Models.Dokumen> dokumenList;

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}