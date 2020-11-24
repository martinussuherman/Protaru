using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKsnT52
{
    [Authorize(Permissions.RtrKsnT52.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Rtr { get; set; }

        [BindProperty]
        public int KodeReferensiAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KodeReferensiAtr = (int)id;
            Rtr = await _context.Atr
                .Include(a => a.Kawasan)
                .FirstOrDefaultAsync(m => m.Kode == KodeReferensiAtr);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Rtr,
                JenisRtrEnum.RtrKsnT52,
                StatusRevisi.RevisiT52,
                User);
            await rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);
            await rtrUtilities.UpdateReferensiRtr(KodeReferensiAtr);
            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly PomeloDbContext _context;
    }
}