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
            AtrDokumenTindakLanjut = new HashSet<AtrDokumenTindakLanjut>();
        }

        [Key]
        public int Kode { get; set; }

        [
            Required(ErrorMessage = "Nama Dokumen harus diisi."),
            MaxLength(100)
        ]
        public string Nama { get; set; }

        [
            Required(ErrorMessage = "Nomor Urut Dokumen harus diisi."),
            Range(1, Int32.MaxValue, ErrorMessage = "Nomor Urut Dokumen harus > 0.")
        ]
        public int Nomor { get; set; }

        [
            Range(1, Int32.MaxValue, ErrorMessage = "Kelompok Dokumen harus diisi."),
            Display(Name = "Kelompok Dokumen")
        ]
        public int KodeKelompokDokumen { get; set; }

        [
            DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Display(Name = "Ambil Nomor RTR dari Nomor Dokumen ini")
        ]
        public byte AmbilNomor { get; set; }

        [
            DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Display(Name = "Jumlah Tindak Lanjut")
        ]
        public byte JumlahTindakLanjut { get; set; }

        [
            DatabaseGenerated(DatabaseGeneratedOption.Identity),
            Display(Name = "Untuk Publik")
        ]
        public byte UntukPublik { get; set; }

        [
            ForeignKey("KodeKelompokDokumen"),
            Display(Name = "Kelompok Dokumen")
        ]
        public KelompokDokumen KelompokDokumen { get; set; }

        public ICollection<AtrDokumen> AtrDokumen { get; set; }

        [InverseProperty("Dokumen")]
        public virtual ICollection<AtrDokumenTindakLanjut> AtrDokumenTindakLanjut { get; set; }

        [NotMapped]
        public AtrDokumen DetailDokumen { get; set; }
    }
}