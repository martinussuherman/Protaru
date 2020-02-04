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

namespace MonevAtr
{
    public static class ExportHelper
    {
        public static async Task<IActionResult> RtrProvKabKotaExport(
            this PageModel model,
            MonevAtrDbContext context,
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

            worksheet.SetCommonWorksheetHeader(namaRtr);
            worksheet.SetCommonRtrProvKabKotaWorksheetHeader();

            //Body of table  
            //  
            int row = 3;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = (row - 2).ToString();
                worksheet.FillCommonRtrProvKabKotaWorksheet(item, row);
                row++;
            }

            worksheet.SetAutoFit(1, 17);
            return model.SaveWorksheetForDownload(excel, "Export-" + namaRtr + ".xlsx");
        }

        public static async Task<IActionResult> RtrOneFieldExport(
            this PageModel model,
            MonevAtrDbContext context,
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

            worksheet.SetCommonWorksheetHeader(namaRtr);
            worksheet.SetCommonRtrOneFieldWorksheetHeader();

            switch (jenisRtr)
            {
                case JenisRtrEnum.RtrPulauT51:
                case JenisRtrEnum.RtrPulauT52:
                    worksheet.Cells[2, 3].Value = "Pulau";
                    break;
                case JenisRtrEnum.RtrKsnT51:
                case JenisRtrEnum.RtrKsnT52:
                    worksheet.Cells[2, 3].Value = "Kawasan";
                    break;
            }

            //Body of table  
            //  
            int row = 3;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = (row - 2).ToString();

                switch (jenisRtr)
                {
                    case JenisRtrEnum.RtrPulauT51:
                    case JenisRtrEnum.RtrPulauT52:
                        worksheet.Cells[row, 3].Value = item.DisplayNamaPulau;
                        break;
                    case JenisRtrEnum.RtrKsnT51:
                    case JenisRtrEnum.RtrKsnT52:
                        worksheet.Cells[row, 3].Value = item.DisplayNamaKawasan;
                        break;
                }

                worksheet.FillCommonRtrOneFieldWorksheet(item, row);
                row++;
            }

            worksheet.SetAutoFit(1, 17);
            return model.SaveWorksheetForDownload(excel, "Export-" + namaRtr + ".xlsx");
        }

        public static async Task<IActionResult> RtrZeroFieldExport(
            this PageModel model,
            MonevAtrDbContext context,
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

            worksheet.SetCommonWorksheetHeader(namaRtr);
            worksheet.SetCommonRtrZeroFieldWorksheetHeader();

            //Body of table  
            //  
            int row = 3;

            foreach (Atr item in list)
            {
                worksheet.Cells[row, 1].Value = (row - 2).ToString();
                worksheet.FillCommonRtrZeroFieldWorksheet(item, row);
                row++;
            }

            worksheet.SetAutoFit(1, 17);
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

        public static void SetCommonWorksheetHeader(this ExcelWorksheet worksheet, string title)
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

        public static void SetCommonRtrProvKabKotaWorksheetHeader(this ExcelWorksheet worksheet)
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
        }

        public static void FillCommonRtrProvKabKotaWorksheet(
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

        public static void SetCommonRtrOneFieldWorksheetHeader(this ExcelWorksheet worksheet)
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
        }

        public static void FillCommonRtrOneFieldWorksheet(
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

        public static void SetCommonRtrZeroFieldWorksheetHeader(this ExcelWorksheet worksheet)
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
        }

        public static void FillCommonRtrZeroFieldWorksheet(
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

        public static IActionResult SaveWorksheetForDownload(
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

        public static void SetAutoFit(this ExcelWorksheet worksheet, int startCol, int endCol)
        {
            for (int counter = startCol; counter <= endCol; counter++)
            {
                worksheet.Column(counter).AutoFit();
            }
        }
    }
}