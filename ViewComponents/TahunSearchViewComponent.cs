using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.ViewComponents.Rtr
{
    public class TahunSearchViewComponent : ViewComponent
    {
        public TahunSearchViewComponent(PomeloDbContext context)
        {
            _selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(JenisRtrEnum jenisRtr)
        {
            return View(new ViewModel
            {
                Tahun = await _selectListUtilities.TahunAsyncOptional(jenisRtr),
                Label = LabelByRtr(jenisRtr)
            });
        }

        public class ViewModel
        {
            public IEnumerable<SelectListItem> Tahun { get; set; }
            public string Label { get; set; }
        }

        private string LabelByRtr(JenisRtrEnum jenisRtr)
        {
            if (jenisRtr == JenisRtrEnum.RdtrT51 ||
                jenisRtr == JenisRtrEnum.RdtrT52 ||
                jenisRtr == JenisRtrEnum.RtrwT50 ||
                jenisRtr == JenisRtrEnum.RtrwT51 ||
                jenisRtr == JenisRtrEnum.RtrwT52 ||
                jenisRtr == JenisRtrEnum.Rdtr ||
                jenisRtr == JenisRtrEnum.Rtrw)
            {
                return "Tahun Perda";
            }

            return "Tahun Perpres";
        }

        private readonly SelectListUtilities _selectListUtilities;
    }
}