using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class PermasalahanTindakLanjutKeteranganViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Atr rtr)
        {
            return View(rtr);
        }
    }
}