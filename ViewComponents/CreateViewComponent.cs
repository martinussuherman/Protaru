using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class CreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Atr rtr, bool displayAoi)
        {
            return View(new ViewModel
            {
                Rtr = rtr,
                DisplayAoi = displayAoi
            });
        }

        public class ViewModel
        {
            public Atr Rtr { get; set; }

            public bool DisplayAoi { get; set; }
        }
    }
}