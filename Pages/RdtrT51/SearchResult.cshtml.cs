using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using P.Pager;

namespace MonevAtr.Pages.RdtrT51
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IPager<Models.Atr> Hasil { get; set; }

        public IActionResult OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RdtrT51)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        public IActionResult OnGetView([FromQuery] AtrSearch rtr)
        {
            return OnGet(rtr, 1);
        }

        public async Task<IActionResult> OnGetExport([FromQuery] AtrSearch rtr)
        {
            List<Models.Atr> list = await _context.Atr
                .ByJenis(JenisRtrEnum.RdtrT51)
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
            ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add("RDTR-T51");

            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            //Header of table  
            //  
            workSheet.Row(1).Height = 40;
            workSheet.Row(1).Style.Font.Size = 20;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "RDTR-T51";
            workSheet.Row(2).Height = 20;
            workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Cells[2, 1].Value = "No";
            workSheet.Cells[2, 2].Value = "Provinsi";
            workSheet.Cells[2, 3].Value = "Kabupaten/Kota";
            workSheet.Cells[2, 4].Value = "Nama";
            workSheet.Cells[2, 5].Value = "Tahun Perda";
            workSheet.Cells[2, 6].Value = "Progress";

            //Body of table  
            //  
            int row = 3;

            foreach (Models.Atr item in list)
            {
                workSheet.Cells[row, 1].Value = (row - 2).ToString();
                workSheet.Cells[row, 2].Value = item.DisplayNamaProvinsi;
                workSheet.Cells[row, 3].Value = item.DisplayNamaKabupatenKota;
                workSheet.Cells[row, 4].Value = item.Nama;
                workSheet.Cells[row, 5].Value = item.Tahun;
                workSheet.Cells[row, 6].Value = item.ProgressAtr.Nama;
                row++;
            }

            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();

            MemoryStream stream = new MemoryStream();
            excel.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Export-RDTR-T51.xlsx");
        }

        public IActionResult OnGetByProgress([FromQuery] int stage, [FromQuery] int page = 1)
        {
            AtrSearch rtr = new AtrSearch();
            AddProgressByStage(rtr, stage);
            Hasil = _context.Atr
                .ByJenis(JenisRtrEnum.RdtrT51)
                .ByProgressList(rtr.ProgressList)
                .RtrInclude()
                .AsNoTracking()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
            return Page();
        }

        private void AddProgressByStage(AtrSearch rtr, int stage)
        {
            switch (stage)
            {
                case 1:
                    rtr.ProgressList.Add(2);
                    rtr.ProgressList.Add(3);
                    break;
                case 2:
                    rtr.ProgressList.Add(4);
                    break;
                case 3:
                    rtr.ProgressList.Add(5);
                    rtr.ProgressList.Add(6);
                    break;
                case 4:
                    rtr.ProgressList.Add(7);
                    break;
                default:
                    rtr.ProgressList.Add(0);
                    break;
            }
        }

        private readonly MonevAtrDbContext _context;
    }
}