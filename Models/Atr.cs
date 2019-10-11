using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr")]
    public partial class Atr : IKode
    {
        public Atr()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
            DokumenTindakLanjut = new HashSet<AtrDokumenTindakLanjut>();
            // AtrProgressInfo = new HashSet<AtrProgressInfo>();
            RtrFasilitasKegiatan = new HashSet<RtrFasilitasKegiatan>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Kode { get; set; }

        [StringLength(255)]
        public string Nama { get; set; }

        [Column(TypeName = "int(11)")]
        public int? KodeProvinsi { get; set; }

        [Column(TypeName = "int(11)")]
        public int? KodeKabupatenKota { get; set; }

        [Column(TypeName = "int(11)")]
        public int? KodePulau { get; set; }

        [Column(TypeName = "int(11)")]
        public int? KodeKawasan { get; set; }

        [StringLength(50)]
        public string Nomor { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeJenisAtr { get; set; }
        // public int? KodeJenisAtr { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Tanggal { get; set; }

        [Column(TypeName = "int(11)")]
        public int? KodeProgressAtr { get; set; }

        [StringLength(2000)]
        public string Aoi { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Luas { get; set; }

        [Column(TypeName = "year(4)")]
        public short Tahun { get; set; }

        [Column(TypeName = "year(4)")]
        public short TahunPenyusunan { get; set; }

        [Column(TypeName = "tinyint(4)")]
        public byte? StatusRevisi { get; set; }

        [StringLength(2000)]
        public string Permasalahan { get; set; }

        [StringLength(2000)]
        public string TindakLanjut { get; set; }

        [StringLength(2000)]
        public string Keterangan { get; set; }

        [Column(TypeName = "tinyint(4)")]
        public byte SudahDirevisi { get; set; }

        [Column("TL1Status", TypeName = "char(1)")]
        public string TL1Status { get; set; }

        [Column("TL1Keterangan")]
        [StringLength(1000)]
        public string TL1Keterangan { get; set; }

        [Column("TL1FilePath")]
        [StringLength(255)]
        public string TL1FilePath { get; set; }

        [Column("TL2Status", TypeName = "char(1)")]
        public string TL2Status { get; set; }

        [Column("TL2Keterangan")]
        [StringLength(1000)]
        public string TL2Keterangan { get; set; }

        [Column("TL2FilePath")]
        [StringLength(255)]
        public string TL2FilePath { get; set; }

        [Column("TL3Status", TypeName = "char(1)")]
        public string TL3Status { get; set; }

        [Column("TL3Keterangan")]
        [StringLength(1000)]
        public string TL3Keterangan { get; set; }

        [Column("TL3FilePath")]
        [StringLength(255)]
        public string TL3FilePath { get; set; }

        [Column("TL4Status", TypeName = "char(1)")]
        public string TL4Status { get; set; }

        [Column("TL4Keterangan")]
        [StringLength(1000)]
        public string TL4Keterangan { get; set; }

        [Column("TL4FilePath")]
        [StringLength(255)]
        public string TL4FilePath { get; set; }

        [Column("TL5Status", TypeName = "char(1)")]
        public string TL5Status { get; set; }

        [Column("TL5Keterangan")]
        [StringLength(1000)]
        public string TL5Keterangan { get; set; }

        [Column("TL5FilePath")]
        [StringLength(255)]
        public string TL5FilePath { get; set; }
        public DateTimeOffset PembaruanTerakhir { get; set; }

        [Required]
        [StringLength(50)]
        public string PembaruanOleh { get; set; }

        [ForeignKey("KodeJenisAtr")]
        [InverseProperty("Atr")]
        public virtual JenisAtr JenisAtr { get; set; }

        [ForeignKey("KodeKabupatenKota")]
        [InverseProperty("Atr")]
        public virtual KabupatenKota KabupatenKota { get; set; }

        [ForeignKey("KodeProgressAtr")]
        [InverseProperty("Atr")]
        public virtual ProgressAtr ProgressAtr { get; set; }

        [ForeignKey("KodeProvinsi")]
        [InverseProperty("Atr")]
        public virtual Provinsi Provinsi { get; set; }

        [ForeignKey("KodePulau")]
        [InverseProperty("Atr")]
        public virtual Pulau Pulau { get; set; }

        [ForeignKey("KodeKawasan")]
        [InverseProperty("Atr")]
        public virtual Kawasan Kawasan { get; set; }

        [InverseProperty("Atr")]
        public virtual ICollection<AtrDokumen> AtrDokumen { get; set; }

        [InverseProperty("Rtr")]
        public virtual ICollection<AtrDokumenTindakLanjut> DokumenTindakLanjut { get; set; }

        // [InverseProperty("KodeAtrNavigation")]
        // public virtual ICollection<AtrProgressInfo> AtrProgressInfo { get; set; }

        [InverseProperty("Rtr")]
        public virtual ICollection<RtrFasilitasKegiatan> RtrFasilitasKegiatan { get; set; }
    }
}