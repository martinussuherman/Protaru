using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class PulauViewComponent : ViewComponent
    {
        public PulauViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isInput)
        {
            return View(new ViewModel
            {
                IsInput = isInput,
                Pulau = await _selectListUtilities.PulauAsync()
            });
        }

        public class ViewModel
        {
            public bool IsInput { get; set; }
            public IEnumerable<SelectListItem> Pulau { get; set; }
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}