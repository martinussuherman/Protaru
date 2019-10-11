using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("fasilitas_kegiatan")]
    public partial class FasilitasKegiatan
    {
        public FasilitasKegiatan()
        {
            RtrFasilitasKegiatan = new HashSet<RtrFasilitasKegiatan>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Kode { get; set; }

        [Required]
        [StringLength(50)]
        public string Nama { get; set; }

        [InverseProperty("FasilitasKegiatan")]
        public virtual ICollection<RtrFasilitasKegiatan> RtrFasilitasKegiatan { get; set; }

        [NotMapped]
        public RtrFasilitasKegiatan DetailFasilitasKegiatan { get; set; }
    }
}