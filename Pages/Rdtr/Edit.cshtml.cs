using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rdtr
{
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<Models.AtrDokumen> AtrDokumenList { get; set; }

        // [BindProperty]
        // public List<IFormFile> UploadFile { get; set; } = new List<IFormFile>();

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            this.KelompokDokumenList = await (from k in _context.KelompokDokumen where k.KodeJenisAtr == (int) JenisAtrEnum.RdtrPerda orderby k.Nomor select k)
                .Include(k => k.Dokumen)
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
            ViewData["ProgressRdtr"] = await _context.GetSelectListProgressRdtr();
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

        private void FixUploadFiles(HttpContext httpContext)
        {
            int count = 0;

            foreach (Models.AtrDokumen dokumen in this.AtrDokumenList)
            {
                string propertyName = $"AtrDokumen[{count++}].UploadFile";
                // dokumen.UploadFile = httpContext.Request.Form.Files
                //     .Where(i => propertyName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase))
                //     .FirstOrDefault();
            }
        }

        private async void MergeAtrDokumenDenganKelompokDokumen(int? id)
        {
            atrDokumenList = await (from d in _context.AtrDokumen where d.KodeAtr == id select d)
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
                joinedItem = new AtrDokumen();
                joinedItem.KodeAtr = this.Atr.Kode;
                joinedItem.KodeDokumen = dokumen.Kode;
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

        // private async Task<bool> SaveAtrDokumen(int index)
        // {
        //     CopyUploadedFile(index);
        //     return await SaveAtrDokumen(this.AtrDokumenList[index]);
        // }

        private async Task<bool> SaveAtrDokumen(Models.AtrDokumen dokumen)
        {
            if (!dokumen.StatusAda)
            {
                return true;
            }

            Models.Dokumen tabelDokumen = dokumenList.Find(d => d.Kode == dokumen.KodeDokumen);

            if (tabelDokumen.AmbilNomor == 1)
            {
                this.Atr.Nomor = dokumen.Nomor;
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

        private async void CopyUploadedFile(Models.AtrDokumen dokumen)
        {
            // IFormFile file = dokumen.UploadFile;
            IFormFile file = dokumen.UploadFile.File;

            if (String.IsNullOrEmpty(file.FileName) || file.Length == 0)
            {
                return;
            }

            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "upload", file.FileName);
            dokumen.FilePath = file.FileName;

            using(FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        // private async void CopyUploadedFile(int index)
        // {
        //     if (index >= this.UploadFile.Count)
        //     {
        //         return;
        //     }

        //     IFormFile file = this.UploadFile[index];

        //     if (String.IsNullOrEmpty(file.FileName) || file.Length == 0)
        //     {
        //         return;
        //     }

        //     string filePath = Path.Combine(hostingEnvironment.WebRootPath, "upload", file.FileName);
        //     this.AtrDokumenList[index].FilePath = file.FileName;

        //     using(FileStream stream = new FileStream(filePath, FileMode.Create))
        //     {
        //         await file.CopyToAsync(stream);
        //     }
        // }

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

        private readonly MonevAtrDbContext _context;

        private readonly IHostingEnvironment hostingEnvironment;
    }
}