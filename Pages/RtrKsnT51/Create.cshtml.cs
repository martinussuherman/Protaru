using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKsnT51
{
    [Authorize(Permissions.RtrKsnT51.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Rtr { get; set; } = new Models.Atr();

        public async Task<IActionResult> OnGetAsync()
        {
            Rtr.KodeJenisAtr = (int)JenisRtrEnum.RtrKsnT51;
            ViewData["Kawasan"] = await selectListUtilities.Kawasan();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Rtr,
                JenisRtrEnum.RtrKsnT51,
                StatusRevisi.Kosong,
                User);
            await rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);
            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly SelectListUtilities selectListUtilities;
    }
}