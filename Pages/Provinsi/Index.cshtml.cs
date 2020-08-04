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
        public IndexModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IList<Models.Provinsi> Provinsi { get; set; }

        public async Task OnGetAsync()
        {
            Provinsi = await _context.Provinsi
                .OrderBy(p => p.Nama)
                .ToListAsync();
        }

        private readonly PomeloDbContext _context;
    }
}