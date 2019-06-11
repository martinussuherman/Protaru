using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("dokumen")]
    public class Dokumen
    {
        public Dokumen()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Dokumen harus diisi."), MaxLength(100)]
        public string Nama { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Kelompok Dokumen harus diisi."), Display(Name = "Kelompok Dokumen")]
        public int KodeKelompokDokumen { get; set; }

        [ForeignKey("KodeKelompokDokumen"), Display(Name = "Kelompok Dokumen")]
        public KelompokDokumen KelompokDokumen { get; set; }

        public ICollection<AtrDokumen> AtrDokumen { get; set; }
    }
}
