using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.ProgressAtr
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.ProgressAtr> ProgressRtr { get; set; }

        public void OnGet([FromQuery] int page)
        {
            ProgressRtr = _context.ProgressAtr
                .Include(p => p.JenisAtr)
                .OrderBy(p => p.JenisAtr.Nama)
                .ThenBy(p => p.Nomor)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private readonly MonevAtrDbContext _context;
    }
}