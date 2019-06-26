using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KelompokDokumen
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.KelompokDokumen> KelompokDokumen { get; set; }

        public async Task OnGetAsync()
        {
            this.KelompokDokumen = await (from k in _context.KelompokDokumen orderby k.JenisAtr.Nama, k.Nomor select k)
                .Include(k => k.JenisAtr)
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}