using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;

namespace MonevAtr.Pages.RdtrT51
{
    [Authorize(Permissions.RdtrT51.Edit)]
    public class EditModel : PageModel
    {
        public EditModel(
            MonevAtrDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            selectListUtilities = new SelectListUtilities(context);
            rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<AtrDokumen> AtrDokumenList { get; set; }

        [BindProperty]
        public List<RtrFasilitasKegiatan> FasKeg { get; set; }

        [BindProperty]
        public List<AtrDokumenTindakLanjut> DokTin { get; set; }

        // [BindProperty]
        // public List<IFormFile> UploadFile { get; set; } = new List<IFormFile>();

        public List<Models.KelompokDokumen> KelompokDokumenList { get; set; }

        public List<FasilitasKegiatan> FasilitasList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            KelompokDokumenList =
                await rtrUtilities.LoadKelompokDokumenDanDokumen(
                    (int)JenisRtrEnum.RdtrT51);
            FasilitasList = await rtrUtilities.LoadFasilitasKegiatan();

            Atr = await _context.Atr
                .Include(a => a.JenisAtr)
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .FirstOrDefaultAsync(m => m.Kode == id);

            rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                Atr,
                id,
                KelompokDokumenList);
            rtrUtilities.MergeRtrFasilitasKegiatan(Atr, id, FasilitasList);

            ViewData["ProgressRdtr"] =
                await selectListUtilities.ProgressRdtrT51();
            ViewData["StatusRevisi"] = selectListUtilities.StatusRevisiRtrRegular;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return await OnGetAsync(this.Atr.Kode);
            // }

            dokumenList = await _context.Dokumen
                .ToListAsync();

            foreach (AtrDokumen dokumen in AtrDokumenList)
            {
                if (!await rtrUtilities.SaveRtrDokumen(
                        Atr,
                        dokumen,
                        dokumenList))
                {
                    return NotFound();
                }
            }

            foreach (RtrFasilitasKegiatan fasilitas in FasKeg)
            {
                if (!await rtrUtilities.SaveRtrFasilitasKegiatan(
                        Atr,
                        fasilitas))
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

            if (!await rtrUtilities.SaveRtr(Atr, User))
            {
                return NotFound();
            }

            return await OnGetAsync(Atr.Kode);
        }

        private void FixUploadFiles(HttpContext httpContext)
        {
            int count = 0;

            foreach (AtrDokumen dokumen in AtrDokumenList)
            {
                string propertyName = $"AtrDokumen[{count++}].UploadFile";
                // dokumen.UploadFile = httpContext.Request.Form.Files
                //     .Where(i => propertyName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase))
                //     .FirstOrDefault();
            }
        }

        // private async Task<bool> SaveAtrDokumen(int index)
        // {
        //     CopyUploadedFile(index);
        //     return await SaveAtrDokumen(this.AtrDokumenList[index]);
        // }

        private async void CopyUploadedFile(AtrDokumen dokumen)
        {
            // IFormFile file = dokumen.UploadFile;
            IFormFile file = dokumen.UploadFile.File;

            if (String.IsNullOrEmpty(file.FileName) || file.Length == 0)
            {
                return;
            }

            string filePath = Path.Combine(_environment.WebRootPath, "upload", file.FileName);
            dokumen.FilePath = file.FileName;

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
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

        private List<Models.Dokumen> dokumenList;

        private readonly RtrUtilities rtrUtilities;

        private readonly SelectListUtilities selectListUtilities;

        private readonly MonevAtrDbContext _context;

        private readonly IWebHostEnvironment _environment;
    }
}