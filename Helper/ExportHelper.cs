using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Linq;

namespace MonevAtr
{
    public static class ExportHelper
    {
        public static async Task<IActionResult> RtrProvKabKotaExport(
            this PageModel model,
            PomeloDbContext context,
            JenisRtrEnum jenisRtr,
            AtrSearch rtr)
        {
            string namaRtr = RtrName(jenisRtr);

            List<Atr> list = await context.Atr
                .ByJenis(jenisRtr)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToListAsync();

            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(namaRtr);
            RtrUtilities utilities = new RtrUtilities(context);
            List<KelompokDokumen> kelompokDokumen = await utilities
                .LoadKelompokDokumenDanDokumen((int)jenisRtr);

            worksheet.SetCommonWorksheetHeader(namaRtr);
            int startCol = worksheet.SetCommonRtrProvKabKotaWorksheetHeader();
            worksheet.DocumentColumnHeader(kelompokDokumen, startCol);

            //Body of table  
            //  
            int row = 4;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = row - 3;
                worksheet.FillCommonRtrProvKabKotaWorksheet(item, row);
                await utilities.MergeRtrDokumenDenganKelompokDokumen(item, kelompokDokumen);
                worksheet.DocumentColumnFill(kelompokDokumen, row, startCol);
                row++;
            }

            worksheet.SetAutoFit(1, 350);
            return model.SaveWorksheetForDownload(excel, "Export-" + namaRtr + ".xlsx");
        }

        public static async Task<IActionResult> RtrOneFieldExport(
            this PageModel model,
            PomeloDbContext context,
            JenisRtrEnum jenisRtr,
            AtrSearch rtr)
        {
            string namaRtr = RtrName(jenisRtr);

            List<Atr> list = await context.Atr
                .ByJenis(jenisRtr)
                .ByKawasan(rtr.Kawasan)
                .ByPulau(rtr.Pulau)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToListAsync();

            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(namaRtr);
            RtrUtilities utilities = new RtrUtilities(context);
            List<KelompokDokumen> kelompokDokumen = await utilities
                .LoadKelompokDokumenDanDokumen((int)jenisRtr);

            worksheet.SetCommonWorksheetHeader(namaRtr);
            int startCol = worksheet.SetCommonRtrOneFieldWorksheetHeader();
            worksheet.DocumentColumnHeader(kelompokDokumen, startCol);

            if (jenisRtr == JenisRtrEnum.RtrPulauT51 ||
                jenisRtr == JenisRtrEnum.RtrPulauT52)
            {
                worksheet.Cells[2, 3].Value = "Pulau";
            }

            if (jenisRtr == JenisRtrEnum.RtrKsnT51 ||
                jenisRtr == JenisRtrEnum.RtrKsnT52)
            {
                worksheet.Cells[2, 3].Value = "Kawasan";
            }

            //Body of table  
            //  
            int row = 4;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = row - 3;

                if (jenisRtr == JenisRtrEnum.RtrPulauT51 ||
                    jenisRtr == JenisRtrEnum.RtrPulauT52)
                {
                    worksheet.Cells[row, 3].Value = item.DisplayNamaPulau;
                }

                if (jenisRtr == JenisRtrEnum.RtrKsnT51 ||
                    jenisRtr == JenisRtrEnum.RtrKsnT52)
                {
                    worksheet.Cells[row, 3].Value = item.DisplayNamaKawasan;
                }

                worksheet.FillCommonRtrOneFieldWorksheet(item, row);
                await utilities.MergeRtrDokumenDenganKelompokDokumen(item, kelompokDokumen);
                worksheet.DocumentColumnFill(kelompokDokumen, row, startCol);
                row++;
            }

            worksheet.SetAutoFit(1, 350);
            return model.SaveWorksheetForDownload(excel, "Export-" + namaRtr + ".xlsx");
        }

        public static async Task<IActionResult> RtrZeroFieldExport(
            this PageModel model,
            PomeloDbContext context,
            JenisRtrEnum jenisRtr,
            AtrSearch rtr)
        {
            string namaRtr = RtrName(jenisRtr);

            List<Atr> list = await context.Atr
                .ByJenis(jenisRtr)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToListAsync();

            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(namaRtr);
            RtrUtilities utilities = new RtrUtilities(context);
            List<KelompokDokumen> kelompokDokumen = await utilities
                .LoadKelompokDokumenDanDokumen((int)jenisRtr);

            worksheet.SetCommonWorksheetHeader(namaRtr);
            int startCol = worksheet.SetCommonRtrZeroFieldWorksheetHeader();
            worksheet.DocumentColumnHeader(kelompokDokumen, startCol);

            //Body of table  
            //  
            int row = 4;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = row - 3;
                worksheet.FillCommonRtrZeroFieldWorksheet(item, row);
                await utilities.MergeRtrDokumenDenganKelompokDokumen(item, kelompokDokumen);
                worksheet.DocumentColumnFill(kelompokDokumen, row, startCol);
                row++;
            }

            worksheet.SetAutoFit(1, 350);
            return model.SaveWorksheetForDownload(excel, "Export-" + namaRtr + ".xlsx");
        }

        private static string RtrName(JenisRtrEnum jenisRtr)
        {
            switch (jenisRtr)
            {
                case JenisRtrEnum.RdtrT51:
                    return "RDTR-T51";

                case JenisRtrEnum.RdtrT52:
                    return "RDTR-T52";

                case JenisRtrEnum.RtrwT50:
                    return "RTRW-T50";

                case JenisRtrEnum.RtrwT51:
                    return "RTRW-T51";

                case JenisRtrEnum.RtrwT52:
                    return "RTRW-T52";

                case JenisRtrEnum.RtrPulauT51:
                    return "RTR-Pulau-T51";

                case JenisRtrEnum.RtrPulauT52:
                    return "RTR-Pulau-T52";

                case JenisRtrEnum.RtrKsnT51:
                    return "RTR-KSN-T51";

                case JenisRtrEnum.RtrKsnT52:
                    return "RTR-KSN-T52";

                case JenisRtrEnum.RtrKpnT51:
                    return "RDTR-KPN-T51";

                case JenisRtrEnum.RtrKpnT52:
                    return "RDTR-KPN-T52";
            }

            return String.Empty;
        }
        private static void SetCommonWorksheetHeader(this ExcelWorksheet worksheet, string title)
        {
            worksheet.TabColor = System.Drawing.Color.Black;
            worksheet.DefaultRowHeight = 12;

            //Header of table  
            //  
            worksheet.Row(1).Height = 40;
            worksheet.Row(1).Style.Font.Size = 20;
            worksheet.Row(2).Style.Font.Bold = true;
            worksheet.Cells[1, 1].Value = title;

            worksheet.Row(2).Height = 20;
            worksheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Row(2).Style.Font.Bold = true;
        }
        private static int SetCommonRtrProvKabKotaWorksheetHeader(this ExcelWorksheet worksheet)
        {
            worksheet.Cells[2, 1].Value = "No";
            worksheet.Cells[2, 2].Value = "Nama";
            worksheet.Cells[2, 3].Value = "Provinsi";
            worksheet.Cells[2, 4].Value = "Kabupaten/Kota";
            worksheet.Cells[2, 5].Value = "Nomor";
            worksheet.Cells[2, 6].Value = "Tanggal";
            worksheet.Cells[2, 7].Value = "Progress";
            worksheet.Cells[2, 8].Value = "Aoi";
            worksheet.Cells[2, 9].Value = "Luas";
            worksheet.Cells[2, 10].Value = "Tahun";
            worksheet.Cells[2, 11].Value = "Tahun Penyusunan";
            worksheet.Cells[2, 12].Value = "Status Revisi";
            worksheet.Cells[2, 13].Value = "Permasalahan";
            worksheet.Cells[2, 14].Value = "Tindak Lanjut";
            worksheet.Cells[2, 15].Value = "Keterangan";
            worksheet.Cells[2, 16].Value = "Pembaruan Terakhir";
            worksheet.Cells[2, 17].Value = "Pembaruan Oleh";
            return 18;
        }
        private static void FillCommonRtrProvKabKotaWorksheet(
            this ExcelWorksheet worksheet,
            Atr item,
            int row)
        {
            worksheet.Cells[row, 2].Value = item.Nama;
            worksheet.Cells[row, 3].Value = item.DisplayNamaProvinsi;
            worksheet.Cells[row, 4].Value = item.DisplayNamaKabupatenKota;
            worksheet.Cells[row, 5].Value = item.Nomor;
            worksheet.Cells[row, 6].Value = item.Tanggal;
            worksheet.Cells[row, 7].Value = item.DisplayNamaProgress;
            worksheet.Cells[row, 8].Value = item.Aoi;
            worksheet.Cells[row, 9].Value = item.Luas;
            worksheet.Cells[row, 10].Value = item.Tahun;
            worksheet.Cells[row, 11].Value = item.TahunPenyusunan;
            worksheet.Cells[row, 12].Value = item.StatusRevisi;
            worksheet.Cells[row, 13].Value = item.Permasalahan;
            worksheet.Cells[row, 14].Value = item.TindakLanjut;
            worksheet.Cells[row, 15].Value = item.Keterangan;
            worksheet.Cells[row, 16].Value = item.PembaruanTerakhir;
            worksheet.Cells[row, 17].Value = item.PembaruanOleh;
        }
        private static int SetCommonRtrOneFieldWorksheetHeader(this ExcelWorksheet worksheet)
        {
            worksheet.Cells[2, 1].Value = "No";
            worksheet.Cells[2, 2].Value = "Nama";
            worksheet.Cells[2, 4].Value = "Nomor";
            worksheet.Cells[2, 5].Value = "Tanggal";
            worksheet.Cells[2, 6].Value = "Progress";
            worksheet.Cells[2, 7].Value = "Aoi";
            worksheet.Cells[2, 8].Value = "Luas";
            worksheet.Cells[2, 9].Value = "Tahun";
            worksheet.Cells[2, 10].Value = "Tahun Penyusunan";
            worksheet.Cells[2, 11].Value = "Status Revisi";
            worksheet.Cells[2, 12].Value = "Permasalahan";
            worksheet.Cells[2, 13].Value = "Tindak Lanjut";
            worksheet.Cells[2, 14].Value = "Keterangan";
            worksheet.Cells[2, 15].Value = "Pembaruan Terakhir";
            worksheet.Cells[2, 16].Value = "Pembaruan Oleh";
            return 17;
        }
        private static void FillCommonRtrOneFieldWorksheet(
            this ExcelWorksheet worksheet,
            Atr item,
            int row)
        {
            worksheet.Cells[row, 2].Value = item.Nama;
            worksheet.Cells[row, 4].Value = item.Nomor;
            worksheet.Cells[row, 5].Value = item.Tanggal;
            worksheet.Cells[row, 6].Value = item.DisplayNamaProgress;
            worksheet.Cells[row, 7].Value = item.Aoi;
            worksheet.Cells[row, 8].Value = item.Luas;
            worksheet.Cells[row, 9].Value = item.Tahun;
            worksheet.Cells[row, 10].Value = item.TahunPenyusunan;
            worksheet.Cells[row, 11].Value = item.StatusRevisi;
            worksheet.Cells[row, 12].Value = item.Permasalahan;
            worksheet.Cells[row, 13].Value = item.TindakLanjut;
            worksheet.Cells[row, 14].Value = item.Keterangan;
            worksheet.Cells[row, 15].Value = item.PembaruanTerakhir;
            worksheet.Cells[row, 16].Value = item.PembaruanOleh;
        }
        private static int SetCommonRtrZeroFieldWorksheetHeader(this ExcelWorksheet worksheet)
        {
            worksheet.Cells[2, 1].Value = "No";
            worksheet.Cells[2, 2].Value = "Nama";
            worksheet.Cells[2, 3].Value = "Nomor";
            worksheet.Cells[2, 4].Value = "Tanggal";
            worksheet.Cells[2, 5].Value = "Progress";
            worksheet.Cells[2, 6].Value = "Aoi";
            worksheet.Cells[2, 7].Value = "Luas";
            worksheet.Cells[2, 8].Value = "Tahun";
            worksheet.Cells[2, 9].Value = "Tahun Penyusunan";
            worksheet.Cells[2, 10].Value = "Status Revisi";
            worksheet.Cells[2, 11].Value = "Permasalahan";
            worksheet.Cells[2, 12].Value = "Tindak Lanjut";
            worksheet.Cells[2, 13].Value = "Keterangan";
            worksheet.Cells[2, 14].Value = "Pembaruan Terakhir";
            worksheet.Cells[2, 15].Value = "Pembaruan Oleh";
            return 16;
        }
        private static void FillCommonRtrZeroFieldWorksheet(
            this ExcelWorksheet worksheet,
            Atr item,
            int row)
        {
            worksheet.Cells[row, 2].Value = item.Nama;
            worksheet.Cells[row, 3].Value = item.Nomor;
            worksheet.Cells[row, 4].Value = item.Tanggal;
            worksheet.Cells[row, 5].Value = item.DisplayNamaProgress;
            worksheet.Cells[row, 6].Value = item.Aoi;
            worksheet.Cells[row, 7].Value = item.Luas;
            worksheet.Cells[row, 8].Value = item.Tahun;
            worksheet.Cells[row, 9].Value = item.TahunPenyusunan;
            worksheet.Cells[row, 10].Value = item.StatusRevisi;
            worksheet.Cells[row, 11].Value = item.Permasalahan;
            worksheet.Cells[row, 12].Value = item.TindakLanjut;
            worksheet.Cells[row, 13].Value = item.Keterangan;
            worksheet.Cells[row, 14].Value = item.PembaruanTerakhir;
            worksheet.Cells[row, 15].Value = item.PembaruanOleh;
        }
        private static IActionResult SaveWorksheetForDownload(
            this PageModel model,
            ExcelPackage excel,
            string fileName)
        {
            MemoryStream stream = new MemoryStream();
            excel.SaveAs(stream);
            stream.Position = 0;

            return model.File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        private static void SetAutoFit(this ExcelWorksheet worksheet, int startCol, int endCol)
        {
            for (int counter = startCol; counter <= endCol; counter++)
            {
                worksheet.Column(counter).AutoFit();
            }
        }
        private static void DocumentColumnHeader(
            this ExcelWorksheet worksheet,
            List<KelompokDokumen> kelompokDokumen,
            int startCol)
        {
            foreach (Dokumen dokumen in kelompokDokumen.SelectMany(kelompok => kelompok.Dokumen))
            {
                worksheet.Cells[2, startCol].Value = dokumen.Nama;
                worksheet.Cells[2, startCol, 2, startCol + 3].Merge = true;
                worksheet.Cells[3, startCol++].Value = "Nomor";
                worksheet.Cells[3, startCol++].Value = "Tanggal";
                worksheet.Cells[3, startCol++].Value = "Keterangan";
                worksheet.Cells[3, startCol++].Value = "Upload File";
            }
        }
        private static void DocumentColumnFill(
            this ExcelWorksheet worksheet,
            List<KelompokDokumen> kelompokDokumen,
            int row,
            int startCol)
        {
            foreach (Dokumen dokumen in kelompokDokumen.SelectMany(kelompok => kelompok.Dokumen))
            {
                worksheet.Cells[row, startCol++].Value = dokumen.DetailDokumen.Nomor;
                worksheet.Cells[row, startCol++].Value = dokumen.DetailDokumen.DisplayTanggalForView;
                worksheet.Cells[row, startCol++].Value = dokumen.DetailDokumen.Keterangan;
                worksheet.Cells[row, startCol++].Value = dokumen.DetailDokumen.FilePathAda ? "Ada" : "Tidak Ada";
            }
        }
    }
}