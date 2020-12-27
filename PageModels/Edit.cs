using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace Protaru.PageModels
{
    public class Edit : PageModel
    {
        public Edit(PomeloDbContext context)
        {
            _context = context;
            _rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Atr Rtr { get; set; }

        [BindProperty]
        public List<AtrDokumen> Dokumen { get; set; }

        [BindProperty]
        public List<RtrFasilitasKegiatan> FasKeg { get; set; }

        public List<FasilitasKegiatan> FasilitasList { get; set; }

        // [BindProperty]
        // public List<AtrDokumenTindakLanjut> DokTin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Rtr = await _context.Atr
                .RtrIncludeAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);
            FasilitasList = await _rtrUtilities.LoadFasilitasKegiatan();
            await _rtrUtilities.MergeRtrFasilitasKegiatan(Rtr, id, FasilitasList);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Rtr.Kode);
            // }

            List<Dokumen> dokumenList = await _context.Dokumen
                .AsNoTracking()
                .ToListAsync();

            foreach (AtrDokumen dokumen in Dokumen)
            {
                if (!await _rtrUtilities.SaveRtrDokumen(Rtr, dokumen, dokumenList))
                {
                    return NotFound();
                }
            }

            foreach (RtrFasilitasKegiatan fasilitas in FasKeg)
            {
                if (!await _rtrUtilities.SaveRtrFasilitasKegiatan(Rtr, fasilitas))
                {
                    return NotFound();
                }
            }

            if (!await _rtrUtilities.SaveRtr(Rtr, User, EntityState.Modified))
            {
                return NotFound();
            }

            return await OnGetAsync(Rtr.Kode);
        }

        private readonly RtrUtilities _rtrUtilities;
        private readonly PomeloDbContext _context;
    }
}
