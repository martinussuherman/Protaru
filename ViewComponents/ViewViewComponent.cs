using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class ViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RtrDetail detail)
        {
            return View(new ViewModel
            {
                Rtr = detail,
            });
        }

        public class ViewModel
        {
            public RtrDetail Rtr { get; set; }
        }
    }
}