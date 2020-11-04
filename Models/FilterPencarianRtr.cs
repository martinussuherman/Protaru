namespace Protaru.Models
{
    public partial class FilterPencarianRtr
    {
        public int Kode { get; set; }

        public string Nama { get; set; }

        public string Nomor { get; set; }

        public short Tahun { get; set; }

        public byte? StatusRevisi { get; set; }

        public int? KodeJenisRtr { get; set; }

        public int? KodeProgressRtr { get; set; }

        public int? KodeFasilitasKegiatan { get; set; }

        public int? KodeProvinsi { get; set; }

        public int? KodeKabupatenKota { get; set; }

        public int? KodeProvinsiKabupatenKota { get; set; }

        public int? KodeDokumen { get; set; }

        public int? TahunDokumen { get; set; }
    }
}