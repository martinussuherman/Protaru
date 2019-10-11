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

        [Required(ErrorMessage = "{0} harus diisi."), MaxLength(20)]
        public string Nama { get; set; }

        public ICollection<Atr> Atr { get; set; }
    }
}