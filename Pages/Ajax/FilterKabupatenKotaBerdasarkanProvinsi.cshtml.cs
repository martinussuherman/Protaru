using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class FilterKabupatenKotaBerdasarkanProvinsiModel : PageModel
    {
        public FilterKabupatenKotaBerdasarkanProvinsiModel(MonevAtrDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
        }

        public async Task<JsonResult> OnGetAsync(int kodeProvinsi)
        {
            SelectList list = await selectListUtilities.KabupatenKota(kodeProvinsi);

            return new JsonResult(list);
        }

        private readonly SelectListUtilities selectListUtilities;
    }
}