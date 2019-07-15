using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("atr")]
    public class Atr
    {
        public Atr()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
        }

        [Key]
        public int Kode { get; set; }

        [MaxLength(255)]
        public string Nama { get; set; }

        [MaxLength(50)]
        public string Nomor { get; set; }

        public DateTime? Tanggal { get; set; }

        [MaxLength(255)]
        public string Aoi { get; set; }

        public int Luas { get; set; }

        [Display(Name = "Tahun Penyusunan")]
        public short? TahunPenyusunan { get; set; }

        public short Tahun { get; set; }

        public short SudahDirevisi { get; set; }

        [Display(Name = "Status")]
        public byte? StatusRevisi { get; set; }

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

        [NotMapped]
        public string DisplayNamaProvinsi
        {
            get
            {
                if (this.Provinsi != null)
                {
                    return this.Provinsi.Nama;
                }

                if (this.KabupatenKota != null)
                {
                    return this.KabupatenKota.Provinsi.Nama;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaProvinsiKabupatenKota
        {
            get
            {
                if (this.KabupatenKota != null)
                {
                    return this.KabupatenKota.Provinsi.Nama + ", " + this.KabupatenKota.Nama;
                }

                if (this.Provinsi != null)
                {
                    return this.Provinsi.Nama;
                }

                return String.Empty;
            }
        }

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

        [NotMapped]
        public bool TL1StatusYes
        {
            get
            {
                return IsStatusYes(this.TL1Status);
            }
            set
            {
                this.TL1Status = ConvertToStatusString(value);
            }
        }

        [NotMapped]
        public bool TL2StatusYes
        {
            get
            {
                return IsStatusYes(this.TL2Status);
            }
            set
            {
                this.TL2Status = ConvertToStatusString(value);
            }
        }

        [NotMapped]
        public bool TL3StatusYes
        {
            get
            {
                return IsStatusYes(this.TL3Status);
            }
            set
            {
                this.TL3Status = ConvertToStatusString(value);
            }
        }

        [NotMapped]
        public bool TL4StatusYes
        {
            get
            {
                return IsStatusYes(this.TL4Status);
            }
            set
            {
                this.TL4Status = ConvertToStatusString(value);
            }
        }

        [NotMapped]
        public bool TL5StatusYes
        {
            get
            {
                return IsStatusYes(this.TL5Status);
            }
            set
            {
                this.TL5Status = ConvertToStatusString(value);
            }
        }

        private string ConvertToStatusString(bool status)
        {
            if (status)
            {
                return "1";
            }

            return "0";
        }

        private bool IsStatusYes(string status)
        {
            return !String.IsNullOrEmpty(status) && status == "1";
        }
    }
}