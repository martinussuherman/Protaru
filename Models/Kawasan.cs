using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("kawasan")]
    public class Kawasan
    {
        public Kawasan()
        {
            Atr = new HashSet<Atr>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "{0} harus diisi."), MaxLength(300)]
        public string Nama { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        [InverseProperty("Kawasan")]
        public ICollection<Atr> Atr { get; set; }

        [InverseProperty("Kawasan")]
        public virtual ICollection<KawasanKabupatenKota> KawasanKabupatenKota { get; set; }

         [InverseProperty("Kawasan")]
        public virtual ICollection<KawasanProvinsi> KawasanProvinsi { get; set; }
   }
}