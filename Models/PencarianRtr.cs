using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("pencarian_rtr")]
    public class PencarianRtr : IKode
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

        [NotMapped]
        public string DetailPath { get; set; }

        [NotMapped]
        public string NamaStatusRevisi { get; set; }

        [NotMapped]
        public string DisplayNama => Nama ?? String.Empty;

        [NotMapped]
        public string DisplayNamaProvinsi
        {
            get
            {
                if (KodeProvinsi != null)
                {
                    return NamaProvinsi;
                }

                if (KodeKabupatenKota != null)
                {
                    return NamaProvinsiKabupatenKota;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaProvinsiKabupatenKota
        {
            get
            {
                if (KodeKabupatenKota != null)
                {
                    return "Kab/Kota " + NamaKabupatenKota + ", Provinsi " + NamaProvinsiKabupatenKota;
                }

                if (KodeProvinsi != null)
                {
                    return "Provinsi " + NamaProvinsi;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayTanggalDokumen => !TanggalDokumen.HasValue ?
            String.Empty :
            TanggalDokumen.Value.ToString("dd-MM-yyyy");
    }
}