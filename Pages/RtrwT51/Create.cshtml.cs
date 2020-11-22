using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RtrwT51
{
    [Authorize(Permissions.RtrwT51.Create)]
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
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrwT51();
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            TahunPenyusunan = selectListUtilities.InputTahunRequired();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                Rtr,
                JenisRtrEnum.RtrwT51,
                StatusRevisi.RegularT51,
                User);
            await rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);
            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;
        private readonly SelectListUtilities selectListUtilities;
    }
}