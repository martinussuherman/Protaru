using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Helpers;

namespace MonevAtr.Pages.Rtr
{
    public class SearchResultModel : SearchResultPageModel
    {
        public SearchResultModel(PomeloDbContext context)
        {
            _helper = new RtrAddResultHelper(context);
        }

        public string ReturnPage { get; set; }

        public async Task<IActionResult> OnGetAsync(
            [FromQuery] int perda,
            [FromQuery] string returnPage,
            [FromQuery] int page = 1)
        {
            AtrSearch search = new AtrSearch
            {
                Perda = perda
            };

            Hasil = await _helper.PagerListAsync(search, RtrAddResultHelper.AddType.All, page);
            Rtr = search;
            RegulationName = "Perda/Perpres";
            IsDisplayRegulation = search.Perda == 1;
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;
            ReturnPage = returnPage;

            return Page();
        }

        private readonly RtrAddResultHelper _helper;
    }
}