using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Dokumen
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.Dokumen> Dokumen { get; set; }

        public async Task OnGetAsync()
        {
            this.Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen)
                .Include(d => d.KelompokDokumen.JenisAtr)
                .OrderBy(d => d.KelompokDokumen.JenisAtr.Nama)
                .ThenBy(d => d.KelompokDokumen.Nomor)
                .ThenBy(d => d.Nomor)
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}