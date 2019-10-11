using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("area")]
    public partial class Area
    {
        public Area()
        {
            // Provinsi = new HashSet<Provinsi>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Kode { get; set; }

        [Required]
        [StringLength(50)]
        public string Nama { get; set; }

        // [InverseProperty("KodeAreaNavigation")]
        // public virtual ICollection<Provinsi> Provinsi { get; set; }
    }
}