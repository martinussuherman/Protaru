using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class KawasanViewComponent : ViewComponent
    {
        public KawasanViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isInput)
        {
            return View(new ViewModel
            {
                IsInput = isInput,
                Kawasan = await _selectListUtilities.KawasanAsync()
            });
        }

        public class ViewModel
        {
            public bool IsInput { get; set; }
            public IEnumerable<SelectListItem> Kawasan { get; set; }
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}