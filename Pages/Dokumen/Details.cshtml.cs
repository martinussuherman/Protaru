using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Pages.Dokumen
{
    public class DetailsModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public DetailsModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public Models.Dokumen Dokumen { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dokumen = await _context.Dokumen
                .Include(d => d.KelompokDokumen).FirstOrDefaultAsync(m => m.Kode == id);

            if (Dokumen == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
