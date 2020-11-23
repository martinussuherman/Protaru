using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Protaru.ViewComponents.Rtr
{
    public class ProgressViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<SelectListItem> progress)
        {
            return View(progress);
        }
    }
}