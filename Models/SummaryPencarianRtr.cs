using System;
using System.Collections.Generic;

namespace MonevAtr.Models
{
    public class SummaryPencarianRtr
    {
        public int Kode { get; set; }

        public string Nama { get; set; }

        public string Nomor { get; set; }

        // public DateTime? Tanggal { get; set; }

        public short Tahun { get; set; }

        public byte? StatusRevisi { get; set; }

        public string NamaJenisRtr { get; set; }

        public int? KodeProgressRtr { get; set; }

        public string NamaProgressRtr { get; set; }

        public string NamaKabupatenKota { get; set; }

        public string NamaProvinsiKabupatenKota { get; set; }

        public string NamaProvinsi { get; set; }

        public int? KodeDokumen { get; set; }

        public int? TahunDokumen { get; set; }

        public DateTime? TanggalDokumen { get; set; }

        public string DisplayNamaProvinsi
        {
            get
            {
                return this.PencarianRtrList.Count == 0 ?
                    String.Empty :
                    this.PencarianRtrList[0].DisplayNamaProvinsi;
            }
        }

        public List<PencarianRtr> PencarianRtrList { get; } = new List<PencarianRtr>();
    }
}