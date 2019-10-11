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

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "{0} harus diisi."), MaxLength(50, ErrorMessage = "{0} maksimal {1} karakter.")]
        // [Required(ErrorMessage = "Nama Kelompok Dokumen harus diisi."), MaxLength(50)]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Nomor Urut Kelompok Dokumen harus diisi."), Range(1, Int32.MaxValue, ErrorMessage = "Nomor Urut Kelompok Dokumen harus > 0.")]
        public int Nomor { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Jenis ATR harus diisi."), Display(Name = "Jenis ATR")]
        public int KodeJenisAtr { get; set; }

        [ForeignKey("KodeJenisAtr"), Display(Name = "Jenis ATR")]
        public JenisAtr JenisAtr { get; set; }

        [NotMapped]
        public string DisplayNamaJenisAtr
        {
            get
            {
                if (this.JenisAtr != null)
                {
                    return this.Nama + " - " + this.JenisAtr.Nama;
                }

                return this.Nama;
            }
        }

        public ICollection<Dokumen> Dokumen { get; set; }
    }
}