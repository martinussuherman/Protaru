using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Helpers;

namespace MonevAtr.Pages.Search
{
    public class SearchResultDaerahByProgress : SearchResultPageModel
    {
        public SearchResultDaerahByProgress(PomeloDbContext context)
        {
            _context = context;
            _helper = new RtrAddResultHelper(context);
        }

        public async Task<IActionResult> OnGetAsync(
            [FromQuery] AtrSearch rtr,
            [FromQuery] int page = 1)
        {
            Hasil = await _helper.PagerListAsync(rtr, RtrAddResultHelper.AddType.All, page);
            Rtr = rtr;
            RegulationName = "Perda";
            IsDisplayRegulation = (rtr.Perda == 1);
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;

            return Page();
        }

        private readonly PomeloDbContext _context;
        private RtrAddResultHelper _helper;
    }
}