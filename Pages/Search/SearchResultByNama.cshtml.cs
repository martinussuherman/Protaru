using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Search
{
    public class SearchResultByNamaModel : SearchResultPageModel
    {
        public SearchResultByNamaModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .ByNama(rtr.Nama)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);

            Rtr = rtr;
            RegulationName = "Perda/Perpres";
            IsDisplayRegulation = true;
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;

            return Page();
        }

        private readonly PomeloDbContext _context;
    }
}