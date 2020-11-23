using Microsoft.AspNetCore.Mvc;

namespace Protaru.ViewComponents.Rtr
{
    public class LuasViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(decimal luas)
        {
            return View(luas);
        }
    }
}