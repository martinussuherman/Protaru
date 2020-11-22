using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrKpnT51
{
    [Authorize(Permissions.RdtrKpnT51.Create)]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Rtr { get; set; } = new Models.Atr();

        public IEnumerable<SelectListItem> TahunPenyusunan { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrKpnT51();
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            TahunPenyusunan = selectListUtilities.InputTahunRequired();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Rtr,
                JenisRtrEnum.RtrKpnT51,
                StatusRevisi.Kosong,
                User);
            await rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);
            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly SelectListUtilities selectListUtilities;
    }
}