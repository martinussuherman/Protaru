using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rtrwn
{
    public class PerpresModel : PageModel
    {
        public PerpresModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Tahun"] = await selectListUtilities.TahunPerpresRtrwnT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}