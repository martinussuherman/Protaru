using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwT51
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
                    .Where(p => p.KodeJenisAtr == (int)JenisRtrEnum.RtrwT51)
                    .OrderBy(p => p.Nomor)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRtrwT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}