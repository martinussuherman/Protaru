using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Protaru.ViewComponents.Rtr
{
    public class TahunPenyusunanViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<SelectListItem> tahunPenyusunan)
        {
            return View(tahunPenyusunan);
        }
    }
}