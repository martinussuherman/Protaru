using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    public class Provinsi
    {
        public Provinsi()
        {
            Atr = new HashSet<Atr>();
            KabupatenKota = new HashSet<KabupatenKota>();
        }

        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Provinsi harus diisi."), MaxLength(100)]
        public string Nama { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public ICollection<Atr> Atr { get; set; }

        public ICollection<KabupatenKota> KabupatenKota { get; set; }

        [InverseProperty("Provinsi")]
        public virtual ICollection<KawasanProvinsi> KawasanProvinsi { get; set; }
    }
}