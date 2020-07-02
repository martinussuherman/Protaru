using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwnT52
{
    [Authorize(Permissions.RtrwnT52.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public int KodeReferensiAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KodeReferensiAtr = (int) id;

            Atr = await _context.Atr
                .FirstOrDefaultAsync(m => m.Kode == KodeReferensiAtr);

            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrwnT52();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Atr,
                JenisRtrEnum.RtrwnT52,
                StatusRevisi.RevisiT52,
                User);

            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.KodeReferensiAtr);
            // }

            _context.Atr.Attach(Atr);
            _context.Entry(Atr).State = EntityState.Added;
            _ = await _context.SaveChangesAsync();
            rtrUtilities.UpdateReferensiRtr(KodeReferensiAtr);

            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}