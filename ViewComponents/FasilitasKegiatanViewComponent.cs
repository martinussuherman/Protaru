using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class FasilitasKegiatanViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<FasilitasKegiatan> fasilitasList)
        {
            return View(fasilitasList);
        }
    }
}