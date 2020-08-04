using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IList<Models.Kawasan> Kawasan { get; set; }

        public async Task OnGetAsync()
        {
            Kawasan = await _context.Kawasan
                .ToListAsync();
        }

        private readonly PomeloDbContext _context;
    }
}