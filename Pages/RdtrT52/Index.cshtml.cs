using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RdtrT52
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

        public IList<Models.ProgressAtr> ProgressAtr
        {
            get
            {
                return _context.ProgressAtr
                    .Where(p => p.KodeJenisAtr == (int)JenisRtrEnum.RdtrT52)
                    .OrderBy(p => p.Nomor)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRdtrT52();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}