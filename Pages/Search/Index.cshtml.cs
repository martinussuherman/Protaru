using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
{
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IList<Models.JenisAtr> JenisAtr => _context.JenisAtr
            .OrderBy(e => e.Nomor)
            .Where(e => e.Perencanaan == 0)
            .AsNoTracking()
            .ToList();

        public IList<FasilitasKegiatan> FasilitasKegiatan => _context.FasilitasKegiatan
            .OrderBy(f => f.Kode)
            .AsNoTracking()
            .ToList();

        public IList<int> TahunRekomendasiGubernur { get; set; }

        public IList<int> TahunPermohonanPersetujuanSubstansi { get; set; }

        public IList<int> TahunMasukLoket { get; set; }

        public IList<int> TahunRapatLintasSektor { get; set; }

        public IList<int> TahunPersetujuanSubstansi { get; set; }

        public IList<int> TahunPerda { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TahunRekomendasiGubernur =
                await selectListUtilities.TahunRekomendasiGubernurListAsync();
            TahunPermohonanPersetujuanSubstansi =
                await selectListUtilities.TahunPermohonanPersetujuanSubstansiListAsync();
            TahunMasukLoket =
                await selectListUtilities.TahunMasukLoketListAsync();
            TahunRapatLintasSektor =
                await selectListUtilities.TahunRapatLintasSektorListAsync();
            TahunPersetujuanSubstansi =
                await selectListUtilities.TahunPersetujuanSubstansiListAsync();
            TahunPerda =
                await selectListUtilities.TahunAsync(JenisRtrEnum.All);

            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}