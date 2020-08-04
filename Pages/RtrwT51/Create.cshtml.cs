using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrwT51();
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            rtrUtilities.SetCommonRtrPropertiesOnCreate(
                this.Atr,
                JenisRtrEnum.RtrwT51,
                StatusRevisi.RegularT51,
                User);

            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync();
            // }

            _context.Atr.Attach(this.Atr);
            _context.Entry(this.Atr).State = EntityState.Added;
            _ = await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}