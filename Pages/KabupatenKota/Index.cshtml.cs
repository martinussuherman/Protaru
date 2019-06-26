using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KabupatenKota
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.KabupatenKota> KabupatenKota { get; set; }

        public async Task OnGetAsync()
        {
            this.KabupatenKota = await (from k in _context.KabupatenKota orderby k.Provinsi.Nama, k.Nama select k)
                .Include(k => k.Provinsi)
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}