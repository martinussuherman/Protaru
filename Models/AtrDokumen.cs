﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr_dokumen")]
    public class AtrDokumen : IKode
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

        public DateTime Tanggal { get; set; } = DateTime.MinValue;

        [MaxLength(1000)]
        public string Keterangan { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }

        [ForeignKey("KodeAtr")]
        public Atr Atr { get; set; }

        [ForeignKey("KodeDokumen")]
        public Dokumen Dokumen { get; set; }

        [NotMapped]
        public string DisplayTanggal =>
            Tanggal == DateTime.MinValue ?
            string.Empty :
            Tanggal.ToString("yyyy-MM-dd");

        [NotMapped]
        public string DisplayTanggalForView =>
            Tanggal == DateTime.MinValue ?
            string.Empty :
            Tanggal.ToString("dd-MM-yyyy");

        [NotMapped]
        public bool StatusAda => !string.IsNullOrEmpty(Status) && Status == "1";

        [NotMapped]
        public bool FilePathAda => !string.IsNullOrEmpty(FilePath);

        [NotMapped]
        public bool PerluSimpan =>
            !string.IsNullOrEmpty(Nomor) ||
            Tanggal != DateTime.MinValue ||
            FilePathAda ||
            StatusAda;
    }
}