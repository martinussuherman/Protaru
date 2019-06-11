using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("kelompok_dokumen")]
    public class KelompokDokumen
    {
        public KelompokDokumen()
        {
            Dokumen = new HashSet<Dokumen>();
        }

        public KelompokDokumen(int kode, string nama)
        {
            this.Kode = kode;
            this.Nama = nama;
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Kelompok Dokumen harus diisi."), MaxLength(50)]
        public string Nama { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Jenis ATR harus diisi."), Display(Name = "Jenis ATR")]
        public int KodeJenisAtr { get; set; }

        [ForeignKey("KodeJenisAtr"), Display(Name = "Jenis ATR")]
        public JenisAtr JenisAtr { get; set; }

        public ICollection<Dokumen> Dokumen { get; set; }
    }
}
