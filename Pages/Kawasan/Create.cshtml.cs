using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
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

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["KabupatenKota"] = await _selectListUtilities.KabupatenKota();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Kawasan.Add(Kawasan);
            await _context.SaveChangesAsync();

            AddKabupatenKotaToContext(KabupatenKota1);
            AddKabupatenKotaToContext(KabupatenKota2);
            AddKabupatenKotaToContext(KabupatenKota3);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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

        private readonly SelectListUtilities _selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}