using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class ProvinsiKabKotaViewComponent : ViewComponent
    {
        public ProvinsiKabKotaViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isInput)
        {
            return View(new ViewModel
            {
                IsInput = isInput,
                Provinsi = await _selectListUtilities.ProvinsiAsync(),
                KabupatenKota = await _selectListUtilities.KabupatenKotaAsync()
            });
        }

        public class ViewModel
        {
            public bool IsInput { get; set; }
            public IEnumerable<SelectListItem> Provinsi { get; set; }
            public IEnumerable<SelectListItem> KabupatenKota { get; set; }
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}