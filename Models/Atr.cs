using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MonevAtr.Models
{
    [Table("atr")]
    public class Atr
    {
        public Atr()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
        }

        [Key]
        public int Kode { get; set; }

        [MaxLength(255)]
        public string Nama { get; set; }

        [Display(Name = "Provinsi")]
        public int KodeProvinsi { get; set; }

        [Display(Name = "Kabupaten/Kota")]
        public int KodeKabupatenKota { get; set; }

        [MaxLength(50)]
        public string Nomor { get; set; }

        [Display(Name = "Jenis ATR")]
        public int KodeJenisAtr { get; set; }

        public DateTime Tanggal { get; set; }

        [Display(Name = "Progress")]
        public int KodeProgressAtr { get; set; }

        public string Aoi { get; set; }

        public int Luas { get; set; }

        public short Tahun { get; set; }

        [ForeignKey("KodeJenisAtr")]
        public JenisAtr JenisAtr { get; set; }

        [ForeignKey("KodeProgressAtr")]
        public ProgressAtr ProgressAtr { get; set; }

        [ForeignKey("KodeKabupatenKota")]
        public KabupatenKota KabupatenKota { get; set; }

        [ForeignKey("KodeProvinsi")]
        public Provinsi Provinsi { get; set; }

        public ICollection<AtrDokumen> AtrDokumen { get; set; }
    }
}
