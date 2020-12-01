using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class ProgressMultiSelectViewComponent : ViewComponent
    {
        public ProgressMultiSelectViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(JenisRtrEnum jenisRtr)
        {
            return View(new ViewModel
            {
                Progress = await _selectListUtilities.ProgressRtrAsync((int)jenisRtr)
            });
        }

        public class ViewModel
        {
            public IList<ProgressAtr> Progress { get; set; }
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}