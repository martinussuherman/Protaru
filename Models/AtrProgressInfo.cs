using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr_progress_info")]
    public partial class AtrProgressInfo
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Kode { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeAtr { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeProgressAtr { get; set; }

        [StringLength(1000)]
        public string Permasalahan { get; set; }

        [StringLength(1000)]
        public string TindakLanjut { get; set; }

        [StringLength(1000)]
        public string Keterangan { get; set; }

        [StringLength(255)]
        public string FilePath { get; set; }

        // [ForeignKey("KodeAtr")]
        // [InverseProperty("AtrProgressInfo")]
        // public virtual Atr KodeAtrNavigation { get; set; }

        // [ForeignKey("KodeProgressAtr")]
        // [InverseProperty("AtrProgressInfo")]
        // public virtual ProgressAtr KodeProgressAtrNavigation { get; set; }
    }
}