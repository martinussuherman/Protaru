using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KabupatenKota
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.KabupatenKota KabupatenKota { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.KabupatenKota = await _context.KabupatenKota
                .Include(k => k.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (this.KabupatenKota == null)
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

            this.KabupatenKota = await _context.KabupatenKota.FindAsync(id);

            if (this.KabupatenKota != null)
            {
                _context.KabupatenKota.Remove(this.KabupatenKota);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}