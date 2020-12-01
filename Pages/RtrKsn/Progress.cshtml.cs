using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrKsn
{
    public class ProgressModel : PageModel
    {
        public AtrSearch Rtr { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}