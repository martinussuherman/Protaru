using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrKsn
{
    public class PerpresModel : PageModel
    {
        public PerpresModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Tahun"] = await selectListUtilities.TahunPerpresRtrKsnT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}