using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKsnT52
{
    [Authorize(Permissions.RtrKsnT52.Edit)]
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<AtrDokumen> AtrDokumenList { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int) JenisRtrEnum.RtrKsnT52);

            Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Kawasan)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Atr,
                id,
                KelompokDokumenList);

            ViewData["Progress"] = await selectListUtilities.ProgressRtrKsnT52();
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

            foreach (AtrDokumen dokumen in AtrDokumenList)
            {
                if (!await rtrUtilities.SaveRtrDokumen(Atr, dokumen, dokumenList))
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

        private List<Dokumen> dokumenList;

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}