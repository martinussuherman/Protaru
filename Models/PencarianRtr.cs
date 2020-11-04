using System;
using MonevAtr.Models;

namespace Protaru.Models
{
    public partial class PencarianRtr : IKode
    {
        public int Kode { get; set; }

        public string Nama { get; set; }

        public string Nomor { get; set; }

        // public DateTime? Tanggal { get; set; }

        public short Tahun { get; set; }

        public sbyte? StatusRevisi { get; set; }

        public int? KodeJenisRtr { get; set; }

        public string NamaJenisRtr { get; set; }

        public int? KodeProgressRtr { get; set; }

        public string NamaProgressRtr { get; set; }

        public int? KodeKabupatenKota { get; set; }

        public string NamaKabupatenKota { get; set; }

        public int? KodeProvinsiKabupatenKota { get; set; }

        public string NamaProvinsiKabupatenKota { get; set; }

        public int? KodeProvinsi { get; set; }

        public string NamaProvinsi { get; set; }

        public int? KodeDokumen { get; set; }

        public int? TahunDokumen { get; set; }

        public int? BulanDokumen { get; set; }

        public DateTime? TanggalDokumen { get; set; }
    }
}