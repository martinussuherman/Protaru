using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public Models.Atr Rtr { get; set; }

        [BindProperty]
        public List<AtrDokumen> Dokumen { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public IEnumerable<SelectListItem> TahunPenyusunan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList = await rtrUtilities.LoadKelompokDokumenDanDokumen(
                (int)JenisRtrEnum.RtrKpnT52);

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

            ViewData["Progress"] = await selectListUtilities.ProgressRtrKpnT52();
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrRevisi;
            TahunPenyusunan = selectListUtilities.InputTahunRequired(Rtr.TahunPenyusunan);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Atr.Kode);
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