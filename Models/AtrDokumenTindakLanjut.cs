using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr_dokumen_tindak_lanjut")]
    public partial class AtrDokumenTindakLanjut
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Kode { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeAtr { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeDokumen { get; set; }

        [Column(TypeName = "smallint(6)")]
        public short Nomor { get; set; }

        [Column(TypeName = "char(1)")]
        public string Status { get; set; }

        [StringLength(1000)]
        public string Keterangan { get; set; }

        [StringLength(255)]
        public string FilePath { get; set; }

        [ForeignKey("KodeAtr")]
        [InverseProperty("DokumenTindakLanjut")]
        public virtual Atr Rtr { get; set; }

        [ForeignKey("KodeDokumen")]
        [InverseProperty("AtrDokumenTindakLanjut")]
        public virtual Dokumen Dokumen { get; set; }
    }
}