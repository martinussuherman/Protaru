using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.KelompokDokumen
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.KelompokDokumen> KelompokDokumen { get; set; }

        public void OnGet([FromQuery] int page)
        {
            KelompokDokumen = _context.KelompokDokumen
                .Include(k => k.JenisAtr)
                .OrderBy(k => k.JenisAtr.Nama)
                .ThenBy(k => k.Nomor)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private readonly MonevAtrDbContext _context;
    }
}