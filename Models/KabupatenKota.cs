using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("kabupaten_kota")]
    public class KabupatenKota
    {
        public KabupatenKota()
        {
            Atr = new HashSet<Atr>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Kabupaten/Kota harus diisi."), MaxLength(100)]
        public string Nama { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Provinsi harus diisi."), Display(Name = "Provinsi")]
        public int KodeProvinsi { get; set; }

        [ForeignKey("KodeProvinsi"), Display(Name = "Provinsi")]
        public Provinsi Provinsi { get; set; }

        public ICollection<Atr> Atr { get; set; }
    }
}