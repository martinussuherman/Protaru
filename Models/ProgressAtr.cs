using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("progress_atr")]
    public class ProgressAtr
    {
        public ProgressAtr()
        {
            Atr = new HashSet<Atr>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Progress ATR harus diisi."), MaxLength(50)]
        public string Nama { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Jenis ATR harus diisi."), Display(Name = "Jenis ATR")]
        public int KodeJenisAtr { get; set; }

        [ForeignKey("KodeJenisAtr"), Display(Name = "Jenis ATR")]
        public JenisAtr JenisAtr { get; set; }

        public ICollection<Atr> Atr { get; set; }
    }
}
