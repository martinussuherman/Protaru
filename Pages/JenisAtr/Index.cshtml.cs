using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.JenisAtr> JenisAtr { get; set; }

        public async Task OnGetAsync()
        {
            this.JenisAtr = await _context.JenisAtr
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}