using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonevAtr.Models
{
    public class AtrSearch
    {
        [MaxLength(255)]
        public string Nama { get; set; }

        [MaxLength(50)]
        public string Nomor { get; set; }

        public string Aoi { get; set; }

        public short Tahun { get; set; }

        [Display(Name = "Kabupaten/Kota")]
        public int KabKota { get; set; }

        [Display(Name = "Provinsi")]
        public int Prov { get; set; }

        public int Pulau { get; set; }

        public int Kawasan { get; set; }

        public List<int> ProgressList { get; set; } = new List<int>();

        public List<int> KodeDokumenList { get; set; } = new List<int>();

        public List<int> JenisList { get; set; } = new List<int>();

        public List<int> Faskeg { get; set; } = new List<int>();

        public List<int> RekGubList { get; set; } = new List<int>();

        public List<int> PerPerSubList { get; set; } = new List<int>();

        public List<int> MasLokList { get; set; } = new List<int>();

        public List<int> LinSekList { get; set; } = new List<int>();

        public List<int> PerSubList { get; set; } = new List<int>();

        public List<int> PerdaList { get; set; } = new List<int>();

        public int KodeDokumen { get; set; }

        public int RekGub { get; set; }

        public int PerPerSub { get; set; }

        public int MasLok { get; set; }

        public int LinSek { get; set; }

        public int PerSub { get; set; }
    }
}