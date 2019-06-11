using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr_dokumen")]
    public class AtrDokumen
    {
        [Key]
        public int Kode { get; set; }

        [Display(Name = "ATR")]
        public int KodeAtr { get; set; }

        [Display(Name = "Dokumen")]
        public int KodeDokumen { get; set; }

        [MaxLength(1)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string Nomor { get; set; }

        public DateTime Tanggal { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }

        [ForeignKey("KodeAtr")]
        public Atr Atr { get; set; }

        [ForeignKey("KodeDokumen")]
        public Dokumen Dokumen { get; set; }
    }
}
