using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwnT51
{
    [Authorize(Permissions.RtrwnT51.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Rtr { get; set; }

        public IEnumerable<SelectListItem> TahunPenyusunan { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrwnT51();
            TahunPenyusunan = selectListUtilities.InputTahunRequired();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Rtr,
                JenisRtrEnum.RtrwnT51,
                StatusRevisi.RegularT51,
                User);
            await rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);
            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly SelectListUtilities selectListUtilities;
    }
}