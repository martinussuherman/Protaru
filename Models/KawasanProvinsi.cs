using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("kawasan_provinsi")]
    public partial class KawasanProvinsi
    {
        [Column(TypeName = "int(11)")]
        public int KodeKawasan { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeProvinsi { get; set; }

        [ForeignKey("KodeProvinsi")]
        [InverseProperty("KawasanProvinsi")]
        public virtual Provinsi Provinsi { get; set; }

        [ForeignKey("KodeKawasan")]
        [InverseProperty("KawasanProvinsi")]
        public virtual Kawasan Kawasan { get; set; }
    }
}