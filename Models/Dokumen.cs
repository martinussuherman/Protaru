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

        [Required(ErrorMessage = "Nomor Urut Dokumen harus diisi."), Range(1, Int32.MaxValue, ErrorMessage = "Nomor Urut Dokumen harus > 0.")]
        public int Nomor { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Kelompok Dokumen harus diisi."), Display(Name = "Kelompok Dokumen")]
        public int KodeKelompokDokumen { get; set; }

        [Display(Name = "Nomor ATR dari Nomor ini")]
        public byte AmbilNomor { get; set; }

        [Display(Name = "Jumlah Tindak Lanjut")]
        public byte JumlahTindakLanjut { get; set; }

        [ForeignKey("KodeKelompokDokumen"), Display(Name = "Kelompok Dokumen")]
        public KelompokDokumen KelompokDokumen { get; set; }

        public ICollection<AtrDokumen> AtrDokumen { get; set; }

        [NotMapped]
        public AtrDokumen DetailDokumen { get; set; }
    }
}