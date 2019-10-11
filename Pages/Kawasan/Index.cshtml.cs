using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.Kawasan> Kawasan { get; set; }

        public async Task OnGetAsync()
        {
            Kawasan = await _context.Kawasan
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}