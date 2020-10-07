using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("pulau")]
    public class Pulau
    {
        public Pulau()
        {
            Atr = new HashSet<Atr>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Pulau harus diisi."), MaxLength(100)]
        public string Nama { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public ICollection<Atr> Atr { get; set; }
    }
}