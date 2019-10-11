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

namespace MonevAtr.Pages.RtrwT51
{
    [Authorize]
    public class ImportModel : PageModel
    {
        public ImportModel(MonevAtrDbContext context,
            IHostingEnvironment environment)
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

            using(FileStream stream = new FileStream(
                Path.Combine(
                    hostingEnvironment.WebRootPath,
                    "upload/impor-rtrw-t51",
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
                hostingEnvironment.WebRootPath,
                "upload/impor-rtrw-t51",
                this.ImportFile.FileName));
            List<Models.ProgressAtr> progressList = await _context.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int) JenisRtrEnum.RtrwT51)
                .OrderBy(p => p.Nomor)
                .ToListAsync();

            using(ExcelPackage package = new ExcelPackage(file))
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
                    _ = await _context.SaveChangesAsync();

                    UpdateAtrDenganDataPerda(atr, perda);
                }
            }

            return true;
        }

        private async void UpdateAtrDenganDataPerda(Models.Atr atr, Models.AtrDokumen perda)
        {
            if (String.IsNullOrEmpty(perda.Nomor))
            {
                return;
            }

            atr.Nomor = perda.Nomor;
            atr.Tahun = (short) perda.Tanggal.Year;

            _context.Atr.Attach(atr);
            _context.Entry(atr).Property(r => r.Nomor).IsModified = true;
            _context.Entry(atr).Property(r => r.Tahun).IsModified = true;
            _ = await _context.SaveChangesAsync();
        }

        private Models.Atr ParseAtr(List<Models.ProgressAtr> progressList,
            ExcelRange cells, int row)
        {
            Models.Atr atr = new Models.Atr
            {
                KodeJenisAtr = (int) JenisRtrEnum.RtrwT51,
                KodeProvinsi = null,
                StatusRevisi = (byte) StatusRevisi.RegularT51.Kode,
                KodeKabupatenKota = utilities.ParseExcelNumber(cells[row, 2]),
                Nama = String.Empty,
                Aoi = utilities.ParseExcelString(cells[row, 3]),
                Luas = utilities.ParseExcelNumber(cells[row, 4]),
                TahunPenyusunan = (short) utilities.ParseExcelNumber(cells[row, 5]),
                KodeProgressAtr = utilities.ParseProgress(progressList, cells, row, 6),
                TL1Status = utilities.ParseExcelString(cells[row, 169]),
                TL1Keterangan = utilities.ParseExcelString(cells[row, 170]),
                TL1FilePath = utilities.ParseExcelString(cells[row, 171]),
                TL2Status = utilities.ParseExcelString(cells[row, 172]),
                TL2Keterangan = utilities.ParseExcelString(cells[row, 173]),
                TL2FilePath = utilities.ParseExcelString(cells[row, 174]),
                TL3Status = utilities.ParseExcelString(cells[row, 175]),
                TL3Keterangan = utilities.ParseExcelString(cells[row, 176]),
                TL3FilePath = utilities.ParseExcelString(cells[row, 177]),
                TL4Status = utilities.ParseExcelString(cells[row, 178]),
                TL4Keterangan = utilities.ParseExcelString(cells[row, 179]),
                TL4FilePath = utilities.ParseExcelString(cells[row, 180]),
                TL5Status = utilities.ParseExcelString(cells[row, 181]),
                TL5Keterangan = utilities.ParseExcelString(cells[row, 182]),
                TL5FilePath = utilities.ParseExcelString(cells[row, 183]),
                Permasalahan = utilities.ParseExcelString(cells[row, 209]),
                TindakLanjut = utilities.ParseExcelString(cells[row, 210]),
                Keterangan = utilities.ParseExcelString(cells[row, 211]),
                PembaruanOleh = User.Identity.Name
            };

            return atr;
        }

        private Models.AtrDokumen ParseBukuFA(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 24, 36);
        }

        private Models.AtrDokumen ParseBukuRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 29, 37);
        }

        private Models.AtrDokumen ParseNaskahAkademis(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 34, 38);
        }

        private Models.AtrDokumen ParseRaperda(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 39, 39);
        }

        private Models.AtrDokumen ParsePetaDasar(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 44, 40);
        }

        private Models.AtrDokumen ParsePetaTematik(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 49, 41);
        }

        private Models.AtrDokumen ParsePetaRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 54, 42);
        }

        private Models.AtrDokumen ParseKajianLH(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 59, 43);
        }

        private Models.AtrDokumen ParseCSRT(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 64, 44);
        }

        private Models.AtrDokumen ParseOrtho(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 69, 45);
        }

        private Models.AtrDokumen ParseSuratPetaDasar(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 74, 46);
        }

        private Models.AtrDokumen ParseSuratPetaTematik(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 79, 47);
        }

        private Models.AtrDokumen ParseSuratPetaRencana(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 84, 48);
        }

        private Models.AtrDokumen ParseSuratBadanGeospasial(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 89, 49);
        }

        private Models.AtrDokumen ParsePenetapanDelineasi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 94, 50);
        }

        private Models.AtrDokumen ParsePenjaminanKualitas(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 99, 51);
        }

        private Models.AtrDokumen ParseKonsultasi1(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 104, 52);
        }

        private Models.AtrDokumen ParseKonsultasi2(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 109, 53);
        }

        private Models.AtrDokumen ParseWilayahBerbatasan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 114, 54);
        }

        private Models.AtrDokumen ParseBKPRD(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 119, 55);
        }

        private Models.AtrDokumen ParseDPRDProvinsi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 124, 56);
        }

        private Models.AtrDokumen ParseValidasiKLHS(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 129, 57);
        }

        private Models.AtrDokumen ParseRekomendasiGubernur(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 134, 58);
        }

        private Models.AtrDokumen ParsePermohonanPersetujuanSubtansi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 139, 59);
        }

        private Models.AtrDokumen ParseDataPertanahan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 144, 60);
        }

        private Models.AtrDokumen ParseDataPerizinan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 149, 61);
        }

        private Models.AtrDokumen ParsePetaParaf(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 154, 62);
        }

        private Models.AtrDokumen ParseMasukLoket(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 159, 63);
        }

        private Models.AtrDokumen ParseLintasSektor(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 164, 64);
        }

        private Models.AtrDokumen ParsePersetujuanSubtansi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 184, 65);
        }

        private Models.AtrDokumen ParsePembahasanKemdagri(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 189, 66);
        }

        private Models.AtrDokumen ParsePembahasanDewan(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 194, 67);
        }

        private Models.AtrDokumen ParseEvaluasiProvinsi(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 199, 68);
        }

        private Models.AtrDokumen ParsePerda(Models.Atr atr, ExcelRange cells, int row)
        {
            return utilities.ParseDokumen(atr, cells, row, 204, 69);
        }

        private readonly ExcelImportUtilities utilities = new ExcelImportUtilities();

        private readonly MonevAtrDbContext _context;

        private readonly IHostingEnvironment hostingEnvironment;
    }
}