using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Itm.Misc
{
    public class CustomPageModel : PageModel
    {
        [ViewData]
        public string Title { get; set; } = String.Empty;

        [ViewData]
        public string PageTitle { get; set; } = String.Empty;
    }
}