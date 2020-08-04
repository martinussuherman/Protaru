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
        public DeleteModel(PomeloDbContext context)
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

            List<KawasanProvinsi> list = await _context.KawasanProvinsi
                .Include(k => k.Provinsi)
                .Where(k => k.KodeKawasan == Kawasan.Kode)
                .ToListAsync();

            Kawasan.KawasanProvinsi = list;

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

            List<KawasanProvinsi> list = await _context.KawasanProvinsi
                .Where(k => k.KodeKawasan == id)
                .ToListAsync();

            _context.KawasanProvinsi.RemoveRange(list);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}