using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrPulauT51
{
    [Authorize(Permissions.RtrPulauT51.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; } = new Models.Atr();

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Pulau"] = await selectListUtilities.Pulau();
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrPulauT51();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Atr,
                JenisRtrEnum.RtrPulauT51,
                StatusRevisi.Kosong,
                User);

            _context.Atr.Attach(Atr);
            _context.Entry(Atr).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}