using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models.Old
{
    [Table("atr")]
    public partial class Atr
    {
        public Atr()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
        }

        [Key]
        public int Kode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), MaxLength(255)]
        public string Nama { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), MaxLength(50)]
        public string Nomor { get; set; }

        public DateTime? Tanggal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), MaxLength(255)]
        public string Aoi { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Luas { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Display(Name = "Tahun Penyusunan")]
        public short TahunPenyusunan { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Tahun { get; set; }

        public short SudahDirevisi { get; set; }

        [Display(Name = "Status")]
        public byte? StatusRevisi { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), MaxLength(50)]
        public string PembaruanOleh { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PembaruanTerakhir { get; set; }

        [MaxLength(1000, ErrorMessage = "Permasalahan maksimal 1000 karakter.")]
        public string Permasalahan { get; set; } = String.Empty;

        [Display(Name = "Tindak Lanjut"), MaxLength(1000, ErrorMessage = "Tindak lanjut maksimal 1000 karakter.")]
        public string TindakLanjut { get; set; } = String.Empty;

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string Keterangan { get; set; } = String.Empty;

        [Display(Name = "Jenis ATR")]
        public int KodeJenisAtr { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Progress ATR harus diisi."), Display(Name = "Progress")]
        public int? KodeProgressAtr { get; set; }

        [Display(Name = "Kabupaten/Kota")]
        public int? KodeKabupatenKota { get; set; }

        [Display(Name = "Provinsi")]
        public int? KodeProvinsi { get; set; }

        [ForeignKey("KodeJenisAtr")]
        public JenisAtr JenisAtr { get; set; }

        [ForeignKey("KodeProgressAtr")]
        public ProgressAtr ProgressAtr { get; set; }

        [ForeignKey("KodeKabupatenKota")]
        public KabupatenKota KabupatenKota { get; set; }

        [ForeignKey("KodeProvinsi")]
        public Provinsi Provinsi { get; set; }

        public ICollection<AtrDokumen> AtrDokumen { get; set; }

        public string TL1Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string TL1Keterangan { get; set; }

        public string TL1FilePath { get; set; }

        public string TL2Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string TL2Keterangan { get; set; }

        public string TL2FilePath { get; set; }

        public string TL3Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string TL3Keterangan { get; set; }

        public string TL3FilePath { get; set; }

        public string TL4Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string TL4Keterangan { get; set; }

        public string TL4FilePath { get; set; }

        public string TL5Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Keterangan maksimal 1000 karakter.")]
        public string TL5Keterangan { get; set; }

        public string TL5FilePath { get; set; }

    }
}