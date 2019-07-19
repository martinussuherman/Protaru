using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRegular
{
    [Authorize]
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<Models.AtrDokumen> AtrDokumenList { get; set; }

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            this.KelompokDokumenList = await _context.KelompokDokumen
                .Include(k => k.Dokumen)
                .Where(k => k.KodeJenisAtr == (int) JenisAtrEnum.RtrwRegular)
                .OrderBy(k => k.Nomor)
                .ToListAsync();

            this.KelompokDokumenList.ForEach(k => k.Dokumen = k.Dokumen
                .OrderBy(d => d.Nomor)
                .ToList());

            this.Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            MergeAtrDokumenDenganKelompokDokumen(id);
            ViewData["Progress"] = await selectListUtilities.ProgressRtrwRegular();
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrwRegular;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Atr.Kode);
            // }

            // FixUploadFiles(HttpContext);

            dokumenList = await _context.Dokumen
                .ToListAsync();

            foreach (Models.AtrDokumen dokumen in this.AtrDokumenList)
            {
                if (!await SaveAtrDokumen(dokumen))
                {
                    return NotFound();
                }
            }

            // for (int index = 0; index < this.AtrDokumenList.Count; index++)
            // {
            //     if (!await SaveAtrDokumen(index))
            //     {
            //         return NotFound();
            //     }
            // }

            if (!await SaveAtr())
            {
                return NotFound();
            }

            return await OnGetAsync(this.Atr.Kode);
        }

        private async void MergeAtrDokumenDenganKelompokDokumen(int? id)
        {
            atrDokumenList = await _context.AtrDokumen
                .Where(d => d.KodeAtr == id)
                .ToListAsync();

            foreach (Models.KelompokDokumen kelompokDokumen in this.KelompokDokumenList)
            {
                foreach (Models.Dokumen dokumen in kelompokDokumen.Dokumen)
                {
                    AdjustItemProperties(dokumen);
                }
            }
        }

        private void AdjustItemProperties(Models.Dokumen dokumen)
        {
            Models.AtrDokumen joinedItem = atrDokumenList.Find(k => k.KodeDokumen == dokumen.Kode);

            if (joinedItem == null)
            {
                joinedItem = new AtrDokumen
                {
                KodeAtr = this.Atr.Kode,
                KodeDokumen = dokumen.Kode
                };
            }

            joinedItem.Atr = this.Atr;
            joinedItem.Dokumen = dokumen;
            dokumen.DetailDokumen = joinedItem;
        }

        private async Task<bool> SaveAtr()
        {
            _context.Attach(this.Atr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtrExists(this.Atr.Kode))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private async Task<bool> SaveAtrDokumen(AtrDokumen dokumen)
        {
            // if (!dokumen.PerluSimpan)
            // {
            //     return true;
            // }

            Models.Dokumen tabelDokumen = dokumenList.Find(d => d.Kode == dokumen.KodeDokumen);

            if (tabelDokumen.AmbilNomor == 1)
            {
                this.Atr.Nomor = dokumen.Nomor;
                this.Atr.Tahun = (short) dokumen.Tanggal.Year;
            }

            if (dokumen.Kode == 0)
            {
                _context.AtrDokumen.Add(dokumen);
                await _context.SaveChangesAsync();
                return true;
            }

            _context.Attach(dokumen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtrDokumenExists(dokumen.Kode))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool AtrExists(int kode)
        {
            return _context.Atr.Any(e => e.Kode == kode);
        }

        private bool AtrDokumenExists(int kode)
        {
            return _context.AtrDokumen.Any(e => e.Kode == kode);
        }

        private List<Models.AtrDokumen> atrDokumenList;

        private List<Models.Dokumen> dokumenList;

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;

        private readonly IHostingEnvironment hostingEnvironment;
    }
}