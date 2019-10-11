using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages_Dokumen
{
    public class IndexModel : PageModel
    {
        private readonly MonevAtrDbContext _context;

        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Dokumen> Dokumen { get; set; }

        public async Task OnGetAsync()
        {
            Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen)
                .Include(d => d.KelompokDokumen.JenisAtr)
                .ToListAsync();
        }
    }
}