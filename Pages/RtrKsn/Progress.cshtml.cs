using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrKsn
{
    public class ProgressModel : PageModel
    {
        public ProgressModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Kawasan"] = await selectListUtilities.Kawasan();
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}