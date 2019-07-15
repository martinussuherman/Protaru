using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRevisi
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public int KodeReferensiAtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            this.KodeReferensiAtr = (int) id;

            this.Atr = await _context.Atr
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .FirstOrDefaultAsync(m => m.Kode == this.KodeReferensiAtr);

            // this.Atr.KodeProgressAtr = 0;

            ViewData["ProgressAtr"] = await _context.GetSelectListProgressRtrwRevisi();
            ViewData["KabupatenKota"] = await _context.GetSelectListKabupatenKota();
            ViewData["Provinsi"] = await _context.GetSelectListProvinsi();

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

            this.Atr.KodeJenisAtr = (int) JenisAtrEnum.RtrwRevisi;
            this.Atr.StatusRevisi = (byte) StatusRevisi.RevisiT52.Kode;
            this.Atr.Tahun = 0;

            if (!ModelState.IsValid)
            {
                return await OnGetAsync(this.KodeReferensiAtr);
            }

            Models.Atr referensi = new Models.Atr()
            {
                Kode = this.KodeReferensiAtr,
                SudahDirevisi = 1
            };

            _context.Atr.Attach(referensi);
            _context.Entry(referensi).Property(r => r.SudahDirevisi).IsModified = true;
            await _context.SaveChangesAsync();

            _context.Atr.Attach(this.Atr);
            _context.Entry(this.Atr).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}