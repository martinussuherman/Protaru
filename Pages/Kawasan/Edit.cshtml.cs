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
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
        {
            _context = context;
            _selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Kawasan Kawasan { get; set; }

        [BindProperty]
        public KawasanKabupatenKota KabupatenKota1 { get; set; }

        [BindProperty]
        public KawasanKabupatenKota KabupatenKota2 { get; set; }

        [BindProperty]
        public KawasanKabupatenKota KabupatenKota3 { get; set; }

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
                .Where(k => k.KodeKawasan == id)
                .ToListAsync();

            KabupatenKota1 = Retrieve(list, 0);
            KabupatenKota2 = Retrieve(list, 1);
            KabupatenKota3 = Retrieve(list, 2);

            ViewData["KabupatenKota"] = await _selectListUtilities.KabupatenKota();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Kawasan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(Kawasan.Kode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            List<KawasanKabupatenKota> list = await _context.KawasanKabupatenKota
                .Where(k => k.KodeKawasan == Kawasan.Kode)
                .ToListAsync();

            _context.KawasanKabupatenKota.RemoveRange(list);
            AddKabupatenKotaToContext(KabupatenKota1);
            AddKabupatenKotaToContext(KabupatenKota2);
            AddKabupatenKotaToContext(KabupatenKota3);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private KawasanKabupatenKota Retrieve(List<KawasanKabupatenKota> list, int index)
        {
            if (index < list.Count)
            {
                return list[index];
            }

            return new KawasanKabupatenKota
            {
                KodeKabupatenKota = 0
            };
        }

        private void AddKabupatenKotaToContext(KawasanKabupatenKota item)
        {
            if (item == null || item.KodeKabupatenKota == 0)
            {
                return;
            }

            item.KodeKawasan = Kawasan.Kode;
            _context.KawasanKabupatenKota.Add(item);
        }

        private bool Exists(int id)
        {
            return _context.Kawasan.Any(e => e.Kode == id);
        }

        private readonly SelectListUtilities _selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}