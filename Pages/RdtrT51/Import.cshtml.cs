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
using OfficeOpenXml;

namespace MonevAtr.Pages.RdtrT51
{
    [Authorize]
    public class ImportModel : PageModel
    {
        public ImportModel(PomeloDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public IFormFile ImportFile { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || !CopyUploadedFile() || !SaveImportData().Result)
            {
                return OnGet();
            }

            return RedirectToPage("./Index");
        }

        private bool CopyUploadedFile()
        {
            if (this.ImportFile == null ||
                String.IsNullOrEmpty(this.ImportFile.FileName) ||
                this.ImportFile.Length == 0)
            {
                return false;
            }

            using (FileStream stream = new FileStream(
                Path.Combine(
                    _environment.WebRootPath,
                    "upload/impor-rdtr",
                    this.ImportFile.FileName),
                FileMode.Create))
            {
                this.ImportFile.CopyTo(stream);
            }

            return true;
        }

        private async Task<bool> SaveImportData()
        {
            FileInfo file = new FileInfo(Path.Combine(
                _environment.WebRootPath,
                "upload/impor-rdtr",
                this.ImportFile.FileName));
            List<Models.ProgressAtr> progressList = await _context.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int)JenisRtrEnum.RdtrT51)
                .OrderBy(p => p.Nomor)
                .ToListAsync();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                for (int row = 8; row <= workSheet.Dimension.End.Row; row++)
                {
                    if (utilities.IsEmptyRow(workSheet.Cells, row))
                    {
                        return true;
                    }

                    Models.Atr atr = ParseAtr(progressList, workSheet.Cells, row);

                    _context.Atr.Attach(atr);
                    _context.Entry(atr).State = EntityState.Added;

                    if (await _context.SaveChangesAsync() == 0)
                    {
                        return false;
                    }

                    List<Models.AtrDokumen> dokumenList = new List<Models.AtrDokumen>
                    {
                        ParseBukuFA(atr, workSheet.Cells, row),
                        ParseBukuRencana(atr, workSheet.Cells, row),
                        ParsePeraturanZonasi(atr, workSheet.Cells, row),
                        ParseNaskahAkademis(atr, workSheet.Cells, row),
                        ParseRaperda(atr, workSheet.Cells, row),
                        ParsePetaDasar(atr, workSheet.Cells, row),
                        ParsePetaTematik(atr, workSheet.Cells, row),
                        ParsePetaRencana(atr, workSheet.Cells, row),
                        ParseKajianLH(atr, workSheet.Cells, row),
                        ParseCSRT(atr, workSheet.Cells, row),
                        ParseOrtho(atr, workSheet.Cells, row),
                        ParseSuratPetaDasar(atr, workSheet.Cells, row),
                        ParseSuratPetaTematik(atr, workSheet.Cells, row),
                        ParseSuratPetaRencana(atr, workSheet.Cells, row),
                        ParseSuratBadanGeospasial(atr, workSheet.Cells, row),
                        ParsePenetapanDelineasi(atr, workSheet.Cells, row),
                        ParsePenjaminanKualitas(atr, workSheet.Cells, row),
                        ParseKonsultasi1(atr, workSheet.Cells, row),
                        ParseKonsultasi2(atr, workSheet.Cells, row),
                        ParseWilayahBerbatasan(atr, workSheet.Cells, row),
                        ParseBKPRD(atr, workSheet.Cells, row),
                        ParseDPRDProvinsi(atr, workSheet.Cells, row),
                        ParseValidasiKLHS(atr, workSheet.Cells, row),
                        ParseRekomendasiGubernur(atr, workSheet.Cells, row),
                        ParsePermohonanPersetujuanSubtansi(atr, workSheet.Cells, row),
                        ParseDataPertanahan(atr, workSheet.Cells, row),
                        ParseDataPerizinan(atr, workSheet.Cells, row),
                        ParsePetaParaf(atr, workSheet.Cells, row),
                        ParseMasukLoket(atr, workSheet.Cells, row),
                        ParseLintasSektor(atr, workSheet.Cells, row),
                        ParsePersetujuanSubtansi(atr, workSheet.Cells, row),
                        ParsePembahasanKemdagri(atr, workSheet.Cells, row),
                        ParsePembahasanDewan(atr, workSheet.Cells, row),
                        ParseEvaluasiProvinsi(atr, workSheet.Cells, row)
                    };

                    Models.AtrDokumen perda = ParsePerda(atr, workSheet.Cells, row);
                    dokumenList.Add(perda);

                    _context.AddRange(dokumenList);
                    await _context.SaveChangesAsync();
                    await UpdateAtrDenganDataPerda(atr, perda);
                }
            }

            return true;
        }

        private async Task UpdateAtrDenganDataPerda(Models.Atr atr, Models.AtrDokumen perda)
        {
            if (String.IsNullOrEmpty(perda.Nomor))
            {
                return;
            }

            atr.Nomor = perda.Nomor;
            atr.Tahun = (short)perda.Tanggal.Year;

            _context.Atr.Attach(atr);
            _context.Entry(atr).Property(r => r.Nomor).IsModified = true;
            _context.Entry(atr).Property(r => r.Tahun).IsModified = true;
            await _context.SaveChangesAsync();
        }

        private Models.Atr ParseAtr(List<Models.ProgressAtr> progressList,
            ExcelRange cells, int row)
        {
            Models.Atr atr = new Models.Atr
            {
                KodeJenisAtr = (int)JenisRtrEnum.RdtrT51,
                KodeProvinsi = null,
                KodeKabupatenKota = utilities.ParseExcelNumber(cells[row, 2]),
                Nama = utilities.ParseExcelString(cells[row, 3]),
                Aoi = utilities.ParseExcelString(cells[row, 4]),
                Luas = utilities.ParseExcelNumber(cells[row, 5]),
                TahunPenyusunan = (short)utilities.ParseExcelNumber(cells[row, 6]),
                KodeProgressAtr = utilities.ParseProgress(progressList, cells, row, 7),
                TL1Status = utilities.ParseExcelString(cells[row, 175]),
                TL1Keterangan = utilities.ParseExcelString(cells[row, 176]),
                TL1FilePath = utilities.ParseExcelString(cells[row, 177]),
                TL2Status = utilities.ParseExcelString(cells[row, 178]),
                TL2Keterangan = utilities.ParseExcelString(cells[row, 179]),
                TL2FilePath = utilities.ParseExcelString(cells[row, 180]),
                TL3Status = utilities.ParseExcelString(cells[row, 181]),
                TL3Keterangan = utilities.ParseExcelString(cells[row, 182]),
                TL3FilePath = utilities.ParseExcelString(cells[row, 183]),
                TL4Status = utilities.ParseExcelString(cells[row, 184]),
                TL4Keterangan = utilities.ParseExcelString(cells[row, 185]),
                TL4FilePath = utilities.ParseExcelString(cells[row, 186]),
                TL5Status = utilities.ParseExcelString(cells[row, 187]),
                TL5Keterangan = utilities.ParseExcelString(cells[row, 188]),
                TL5FilePath = utilities.ParseExcelString(cells[row, 189]),
                Permasalahan = utilities.ParseExcelString(cells[row, 215]),
                TindakLanjut = utilities.ParseExcelString(cells[row, 216]),
                Keterangan = utilities.ParseExcelString(cells[row, 217]),
                PembaruanOleh = User.Identity.Name
            };

            return atr;
        }

        private Models.AtrDokumen ParseBukuFA(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 25, 1);
        }

        private Models.AtrDokumen ParseBukuRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 30, 2);
        }

        private Models.AtrDokumen ParsePeraturanZonasi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 35, 3);
        }

        private Models.AtrDokumen ParseNaskahAkademis(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 40, 4);
        }

        private Models.AtrDokumen ParseRaperda(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 45, 5);
        }

        private Models.AtrDokumen ParsePetaDasar(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 50, 6);
        }

        private Models.AtrDokumen ParsePetaTematik(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 55, 7);
        }

        private Models.AtrDokumen ParsePetaRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 60, 8);
        }

        private Models.AtrDokumen ParseKajianLH(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 65, 9);
        }

        private Models.AtrDokumen ParseCSRT(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 70, 10);
        }

        private Models.AtrDokumen ParseOrtho(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 75, 11);
        }

        private Models.AtrDokumen ParseSuratPetaDasar(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 80, 12);
        }

        private Models.AtrDokumen ParseSuratPetaTematik(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 85, 13);
        }

        private Models.AtrDokumen ParseSuratPetaRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 90, 14);
        }

        private Models.AtrDokumen ParseSuratBadanGeospasial(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 95, 15);
        }

        private Models.AtrDokumen ParsePenetapanDelineasi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 100, 16);
        }

        private Models.AtrDokumen ParsePenjaminanKualitas(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 105, 17);
        }

        private Models.AtrDokumen ParseKonsultasi1(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 110, 18);
        }

        private Models.AtrDokumen ParseKonsultasi2(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 115, 19);
        }

        private Models.AtrDokumen ParseWilayahBerbatasan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 120, 20);
        }

        private Models.AtrDokumen ParseBKPRD(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 125, 21);
        }

        private Models.AtrDokumen ParseDPRDProvinsi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 130, 22);
        }

        private Models.AtrDokumen ParseValidasiKLHS(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 135, 23);
        }

        private Models.AtrDokumen ParseRekomendasiGubernur(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 140, 24);
        }

        private Models.AtrDokumen ParsePermohonanPersetujuanSubtansi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 145, 25);
        }

        private Models.AtrDokumen ParseDataPertanahan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 150, 26);
        }

        private Models.AtrDokumen ParseDataPerizinan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 155, 27);
        }

        private Models.AtrDokumen ParsePetaParaf(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 160, 28);
        }

        private Models.AtrDokumen ParseMasukLoket(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 165, 29);
        }

        private Models.AtrDokumen ParseLintasSektor(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 170, 30);
        }

        private Models.AtrDokumen ParsePersetujuanSubtansi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 190, 31);
        }

        private Models.AtrDokumen ParsePembahasanKemdagri(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 195, 32);
        }

        private Models.AtrDokumen ParsePembahasanDewan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 200, 33);
        }

        private Models.AtrDokumen ParseEvaluasiProvinsi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 205, 34);
        }

        private Models.AtrDokumen ParsePerda(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 210, 35);
        }

        private readonly ExcelImportUtilities utilities = new ExcelImportUtilities();

        private readonly PomeloDbContext _context;

        private readonly IWebHostEnvironment _environment;
    }
}