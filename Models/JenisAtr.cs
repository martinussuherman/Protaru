using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("jenis_atr")]
    public class JenisAtr
    {
        public JenisAtr()
        {
            Atr = new HashSet<Atr>();
            KelompokDokumen = new HashSet<KelompokDokumen>();
            ProgressAtr = new HashSet<ProgressAtr>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Jenis ATR harus diisi."), MaxLength(20)]
        public string Nama { get; set; }

        public int Nomor { get; set; }

        public short Perencanaan { get; set; }

        public ICollection<Atr> Atr { get; set; }

        public ICollection<KelompokDokumen> KelompokDokumen { get; set; }

        public ICollection<ProgressAtr> ProgressAtr { get; set; }
    }
}