using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("rtr_fasilitas_kegiatan")]
    public class RtrFasilitasKegiatan
    {
        [Key]
        public int Kode { get; set; }

        [Display(Name = "RTR")]
        public int KodeRtr { get; set; }

        [Display(Name = "Fasilitas Kegiatan")]
        public int KodeFasilitasKegiatan { get; set; }

        public short Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Tahun { get; set; }

        [MaxLength(1000)]
        public string Keterangan { get; set; }

        [ForeignKey("KodeRtr")]
        [InverseProperty("RtrFasilitasKegiatan")]
        public Atr Rtr { get; set; }

        [ForeignKey("KodeFasilitasKegiatan")]
        [InverseProperty("RtrFasilitasKegiatan")]
        public FasilitasKegiatan FasilitasKegiatan { get; set; }

        [NotMapped]
        public bool IsStatusYes
        {
            get => Status != 0;
            set => Status = (short)(value ? 1 : 0);
        }

        [NotMapped]
        public bool PerluSimpan => IsStatusYes ||
            Tahun != 0 ||
            !String.IsNullOrEmpty(Keterangan);
    }
}