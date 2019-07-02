using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using OfficeOpenXml;

namespace MonevAtr.Pages.Rdtr
{
    public class ImportModel : PageModel
    {
        public ImportModel(MonevAtrDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
        }

        [BindProperty]
        public IFormFile ImportFile { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            if (!CopyUploadedFile())
            {
                return OnGet();
            }

            SaveImportData();

            return RedirectToPage("./Index");
        }

        private bool CopyUploadedFile()
        {
            if (this.ImportFile == null || String.IsNullOrEmpty(this.ImportFile.FileName) || this.ImportFile.Length == 0)
            {
                return false;
            }

            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "upload", this.ImportFile.FileName);

            using(FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                this.ImportFile.CopyTo(stream);
            }

            return true;
        }

        private async void SaveImportData()
        {
            FileInfo file = new FileInfo(Path.Combine(hostingEnvironment.WebRootPath, "upload", this.ImportFile.FileName));

            using(ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                int totalRows = workSheet.Dimension.Rows;

                List<Models.Atr> atrList = new List<Models.Atr>();

                for (int i = 8; i <= totalRows; i++)
                {
                    Models.Atr atr = new Models.Atr();

                    atr.KodeJenisAtr = (int) JenisAtrEnum.RdtrPerda;

                    atr.KodeKabupatenKota = (int) workSheet.Cells[i, 2].Value;
                    atr.Nama = workSheet.Cells[i, 3].Value.ToString();
                    atr.Aoi = workSheet.Cells[i, 4].Value.ToString();
                    atr.Luas = (int) workSheet.Cells[i, 5].Value;
                    atr.Tahun = (short) workSheet.Cells[i, 6].Value;
                    atr.TL1Status = workSheet.Cells[i, 7].Value.ToString();
                    atr.TL1Keterangan = workSheet.Cells[i, 8].Value.ToString();
                    atr.TL1FilePath = workSheet.Cells[i, 9].Value.ToString();
                    atr.TL2Status = workSheet.Cells[i, 10].Value.ToString();
                    atr.TL2Keterangan = workSheet.Cells[i, 11].Value.ToString();
                    atr.TL2FilePath = workSheet.Cells[i, 12].Value.ToString();
                    atr.TL3Status = workSheet.Cells[i, 13].Value.ToString();
                    atr.TL3Keterangan = workSheet.Cells[i, 14].Value.ToString();
                    atr.TL3FilePath = workSheet.Cells[i, 15].Value.ToString();
                    atr.TL4Status = workSheet.Cells[i, 16].Value.ToString();
                    atr.TL4Keterangan = workSheet.Cells[i, 17].Value.ToString();
                    atr.TL4FilePath = workSheet.Cells[i, 18].Value.ToString();
                    atr.TL5Status = workSheet.Cells[i, 19].Value.ToString();
                    atr.TL5Keterangan = workSheet.Cells[i, 20].Value.ToString();
                    atr.TL5FilePath = workSheet.Cells[i, 21].Value.ToString();

                    atrList.Add(atr);
                }

                _context.Atr.AddRange(atrList);
                await _context.SaveChangesAsync();
            }
        }

        private readonly MonevAtrDbContext _context;

        private readonly IHostingEnvironment hostingEnvironment;
    }
}