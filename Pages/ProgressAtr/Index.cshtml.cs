using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.ProgressAtr> ProgressAtr { get; set; }

        public async Task OnGetAsync()
        {
            this.ProgressAtr = await _context.ProgressAtr
                .Include(p => p.JenisAtr)
                .OrderBy(p => p.JenisAtr.Nama)
                .ThenBy(p => p.Nomor)
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}