using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RdtrT51
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IList<Models.ProgressAtr> ProgressAtr
        {
            get
            {
                return _context.ProgressAtr
                    .Where(p => p.KodeJenisAtr == (int) JenisRtrEnum.RdtrT51)
                    .OrderBy(p => p.Nomor)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRdtrT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}