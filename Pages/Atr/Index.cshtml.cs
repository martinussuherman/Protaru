using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Atr
{
    public class IndexModel : PageModel
    {
        private readonly MonevAtrDbContext _context;

        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] int page)
        {
            Hasil = _context.Atr
                .Include(a => a.JenisAtr)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }
    }
}