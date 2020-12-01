using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rtrw
{
    public class PerdaModel : PageModel
    {
        public PerdaModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRtrwT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}