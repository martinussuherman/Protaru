using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    [Table("kawasan_kabupaten_kota")]
    public partial class KawasanKabupatenKota
    {
        [Column(TypeName = "int(11)")]
        public int KodeKawasan { get; set; }

        [Column(TypeName = "int(11)")]
        public int KodeKabupatenKota { get; set; }

        [ForeignKey("KodeKabupatenKota")]
        [InverseProperty("KawasanKabupatenKota")]
        public virtual KabupatenKota KabupatenKota { get; set; }

        [ForeignKey("KodeKawasan")]
        [InverseProperty("KawasanKabupatenKota")]
        public virtual Kawasan Kawasan { get; set; }
    }
}