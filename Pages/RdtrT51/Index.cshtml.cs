using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RdtrT51
{
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Tahun"] = await selectListUtilities.TahunPerdaRdtrT51();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}