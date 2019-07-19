using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRegular
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ProgressAtr"] = await selectListUtilities.ProgressRtrwRegular();
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.Atr.KodeProvinsi == 0)
            {
                this.Atr.KodeProvinsi = null;
            }

            if (this.Atr.KodeKabupatenKota == 0)
            {
                this.Atr.KodeKabupatenKota = null;
            }

            this.Atr.KodeJenisAtr = (int) JenisAtrEnum.RtrwRegular;
            this.Atr.Tahun = 0;
            this.Atr.StatusRevisi = (byte) StatusRevisi.RegularT51.Kode;

            if (!ModelState.IsValid)
            {
                return await OnGetAsync();
            }

            _context.Atr.Attach(this.Atr);
            _context.Entry(this.Atr).State = EntityState.Added;
            _ = await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}