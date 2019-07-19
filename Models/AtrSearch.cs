using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonevAtr.Models
{
    public class AtrSearch
    {
        [MaxLength(255)]
        public string Nama { get; set; }

        [MaxLength(50)]
        public string Nomor { get; set; }

        public string Aoi { get; set; }

        public short Tahun { get; set; }

        [Display(Name = "Kabupaten/Kota")]
        public int KodeKabupatenKota { get; set; }

        [Display(Name = "Provinsi")]
        public int KodeProvinsi { get; set; }

        public List<int> KodeProgressAtr { get; set; } = new List<int>();

        public List<int> KodeDokumenList { get; set; } = new List<int>();

        public List<int> KodeJenisAtrList { get; set; } = new List<int>();

        public int KodeDokumen { get; set; }

        public int TahunRekomendasiGubernur { get; set; }

        public int TahunPermohonanPersetujuanSubstansi { get; set; }

        public int TahunMasukLoket { get; set; }

        public int TahunRapatLintasSektor { get; set; }

        public int TahunPersetujuanSubstansi { get; set; }
    }
}