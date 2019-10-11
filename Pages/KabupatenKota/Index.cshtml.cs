using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.KabupatenKota
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.KabupatenKota> KabupatenKota { get; set; }

        public void OnGet([FromQuery] int page)
        {
            this.KabupatenKota = _context.KabupatenKota
                .Include(k => k.Provinsi)
                .OrderBy(k => k.Provinsi.Nama)
                .ThenBy(k => k.Nama)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private readonly MonevAtrDbContext _context;
    }
}