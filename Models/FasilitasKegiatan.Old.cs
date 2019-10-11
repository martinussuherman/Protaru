using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models.Old
{
    [Table("fasilitas_kegiatan")]
    public class FasilitasKegiatan
    {
        public FasilitasKegiatan()
        {
            Atr = new HashSet<Atr>();
            // KelompokDokumen = new HashSet<KelompokDokumen>();
        }

        [Key]
        public int Kode { get; set; }

        [Required(ErrorMessage = "Nama Fasilitas Kegiatan harus diisi."), MaxLength(50)]
        public string Nama { get; set; }

        public ICollection<Atr> Atr { get; set; }

        [NotMapped]
        public RtrFasilitasKegiatan DetailFasilitasKegiatan { get; set; }
    }
}