using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class StatusRevisiViewComponent : ViewComponent
    {
        public StatusRevisiViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public IViewComponentResult Invoke(bool isRegular, byte sudahDirevisi, byte? status)
        {
            return View(new ViewModel
            {
                Items = isRegular ?
                    _selectListUtilities.StatusRegularItems(status) :
                    _selectListUtilities.StatusRevisiItems(status),
                SudahDirevisi = sudahDirevisi == 1
            });
        }

        public class ViewModel
        {
            public IEnumerable<SelectListItem> Items { get; set; }
            public bool SudahDirevisi { get; set; }
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}