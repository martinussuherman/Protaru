using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rtrwn
{
    public class ProgressModel : PageModel
    {
        public ProgressModel()
        {
        }

        public AtrSearch Rtr { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}