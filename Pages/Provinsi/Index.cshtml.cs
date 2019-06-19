using System.Collections.Generic;
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
            Provinsi = await _context.Provinsi.ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}