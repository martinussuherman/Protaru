using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrPulau
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
            ViewData["Pulau"] = await selectListUtilities.Pulau();
            ViewData["Tahun"] = await selectListUtilities.TahunPerpresRtrPulauT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}