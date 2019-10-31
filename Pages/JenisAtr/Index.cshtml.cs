using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.JenisAtr
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.JenisAtr> JenisRtr { get; set; }

        public void OnGet([FromQuery] int page)
        {
            JenisRtr = _context.JenisAtr
                .OrderBy(q => q.Nomor)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private readonly MonevAtrDbContext _context;
    }
}