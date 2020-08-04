using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class IndexModel : PageModel
    {
        private readonly PomeloDbContext _context;

        public IndexModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IList<Models.Dokumen> Dokumen { get; set; }

        public async Task OnGetAsync()
        {
            Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen)
                .Include(d => d.KelompokDokumen.JenisAtr)
                .ToListAsync();
        }
    }
}