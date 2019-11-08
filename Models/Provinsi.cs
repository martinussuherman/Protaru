using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("provinsi")]
    public class Provinsi
    {
        public Provinsi()
        {
            Atr = new HashSet<Atr>();
            KabupatenKota = new HashSet<KabupatenKota>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Provinsi harus diisi."), MaxLength(100)]
        public string Nama { get; set; }

        public ICollection<Atr> Atr { get; set; }

        public ICollection<KabupatenKota> KabupatenKota { get; set; }

        [InverseProperty("Provinsi")]
        public virtual ICollection<KawasanProvinsi> KawasanProvinsi { get; set; }
    }
}