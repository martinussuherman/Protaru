using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Protaru.Areas.Page
{
    public class ProtaruPageModel : PageModel, ITitlePageModel, ILayoutPageModel, IActiveMenuPageModel
    {
        public string Layout { get; set; } = String.Empty;

        public string PageTitle { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public ActiveMenu ActiveMenu { get; set; }
    }
}