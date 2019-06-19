using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Provinsi
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Provinsi Provinsi { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provinsi = await _context.Provinsi.FirstOrDefaultAsync(m => m.Kode == id);

            if (Provinsi == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Provinsi = await _context.Provinsi.FindAsync(id);

            if (Provinsi != null)
            {
                _context.Provinsi.Remove(Provinsi);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}