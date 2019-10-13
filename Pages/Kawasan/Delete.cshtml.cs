using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public DeleteModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Kawasan Kawasan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kawasan = await _context.Kawasan
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (Kawasan == null)
            {
                return NotFound();
            }

            List<KawasanKabupatenKota> list = await _context.KawasanKabupatenKota
                .Include(k => k.KabupatenKota)
                .Where(k => k.KodeKawasan == Kawasan.Kode)
                .ToListAsync();

            Kawasan.KawasanKabupatenKota = list;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kawasan = await _context.Kawasan.FindAsync(id);

            if (Kawasan != null)
            {
                _context.Kawasan.Remove(Kawasan);
            }

            List<KawasanKabupatenKota> list = await _context.KawasanKabupatenKota
                .Where(k => k.KodeKawasan == id)
                .ToListAsync();

            _context.KawasanKabupatenKota.RemoveRange(list);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}