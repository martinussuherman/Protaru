using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Helpers;

namespace MonevAtr.Pages.Rtrw
{
    public class SearchResultModel : SearchResultPageModel
    {
        public SearchResultModel(PomeloDbContext context)
        {
            _helper = new RtrAddResultHelper(context);
        }

        public string ReturnPage { get; set; }

        public async Task<IActionResult> OnGetAsync(
            [FromQuery] AtrSearch rtr,
            [FromQuery] string returnPage,
            [FromQuery] int page = 1)
        {
            Hasil = await _helper.PagerListAsync(rtr, JenisRtrEnum.Rtrw, page);
            Rtr = rtr;
            RegulationName = "Perda";
            IsDisplayRegulation = rtr.Perda == 1;
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;
            ReturnPage = returnPage;

            return Page();
        }

        private readonly RtrAddResultHelper _helper;
    }
}