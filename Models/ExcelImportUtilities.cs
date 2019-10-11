using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace MonevAtr.Models
{
    public class ExcelImportUtilities
    {
        public AtrDokumen ParseDokumen(Atr atr, ExcelRange cells,
            int row, int startCol, int kodeDokumen)
        {
            AtrDokumen dokumen = new AtrDokumen
            {
                KodeAtr = atr.Kode,
                KodeDokumen = kodeDokumen,
                Status = ParseExcelString(cells[row, startCol]),
                Nomor = ParseExcelString(cells[row, startCol + 1]),
                Tanggal = ParseExcelDate(cells[row, startCol + 2]),
                Keterangan = ParseExcelString(cells[row, startCol + 3]),
                FilePath = ParseExcelString(cells[row, startCol + 4])
            };

            return dokumen;
        }

        public int ParseProgress(List<Models.ProgressAtr> progressList,
            ExcelRange cells, int row, int startCol)
        {
            for (int index = 0; index < progressList.Count; index++)
            {
                if (ParseExcelNumber(cells[row, startCol + (index * 3)]) != 0)
                {
                    return progressList[index].Kode;
                }
            }

            return progressList.Count > 0 ? progressList[0].Kode : 0;
        }

        public DateTime ParseExcelDate(ExcelRange cell)
        {
            return cell == null ||
                cell.Value == null ||
                !Int64.TryParse(cell.Value.ToString(), out long dateLong) ?
                DateTime.MinValue :
                DateTime.FromOADate(dateLong);
        }

        public int ParseExcelNumber(ExcelRange cell)
        {
            return cell == null ||
                cell.Value == null ||
                !Int32.TryParse(cell.Value.ToString(), out int result) ?
                0 :
                result;
        }

        public string ParseExcelString(ExcelRange cell)
        {
            return cell == null ||
                cell.Value == null ?
                String.Empty :
                cell.Value.ToString();
        }

        public bool IsEmptyRow(ExcelRange cells, int row)
        {
            return String.IsNullOrEmpty(cells[row, 1].Text) &&
                String.IsNullOrEmpty(cells[row, 2].Text);
        }
    }
}