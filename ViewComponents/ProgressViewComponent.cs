using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class ProgressViewComponent : ViewComponent
    {
        public ProgressViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(int jenisRtr, int? progress)
        {
            return View(await _selectListUtilities.ProgressRtrAsync(jenisRtr, progress));
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}