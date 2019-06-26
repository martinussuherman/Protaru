using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Provinsi
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.Provinsi> Provinsi { get; set; }

        public async Task OnGetAsync()
        {
            this.Provinsi = await (from p in _context.Provinsi orderby p.Nama select p)
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}