using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.AtrSearch AtrSearch { get; set; }

        public IList<Models.JenisAtr> JenisAtr
        {
            get
            {
                return _context.JenisAtr
                    .OrderBy(p => p.Kode)
                    .ToList();
            }
        }

        // public IList<Models.Dokumen> Dokumen
        // {
        //     get
        //     {
        //         return _context.Dokumen
        //             .Include(p => p.KelompokDokumen)
        //             .Include(p => p.KelompokDokumen.JenisAtr)
        //             .OrderBy(p => p.KelompokDokumen.KodeJenisAtr)
        //             .ThenBy(p => p.KelompokDokumen.Nomor)
        //             .ThenBy(p => p.Nomor)
        //             .ToList();
        //     }
        // }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            ViewData["TahunRekomendasiGubernur"] = await selectListUtilities.TahunRekomendasiGubernur();
            ViewData["TahunPermohonanPersetujuanSubstansi"] = await selectListUtilities.TahunPermohonanPersetujuanSubstansi();
            ViewData["TahunMasukLoket"] = await selectListUtilities.TahunMasukLoket();
            ViewData["TahunRapatLintasSektor"] = await selectListUtilities.TahunRapatLintasSektor();
            ViewData["TahunPersetujuanSubstansi"] = await selectListUtilities.TahunPersetujuanSubstansi();
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRtr();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}