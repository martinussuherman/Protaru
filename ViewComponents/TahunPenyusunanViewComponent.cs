using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class TahunPenyusunanViewComponent : ViewComponent
    {
        public TahunPenyusunanViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public IViewComponentResult Invoke(short tahun)
        {
            return View(_selectListUtilities.InputTahunRequired(tahun));
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}