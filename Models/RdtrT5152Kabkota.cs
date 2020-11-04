namespace Protaru.Models
{
    public partial class RdtrT5152Kabkota
    {
        public int? KodeProvinsi { get; set; }
        public int? KodeKabupatenKota { get; set; }
        public int KodeLama { get; set; }
        public int? KodeBaru { get; set; }
        public string NamaLama { get; set; }
        public string NamaBaru { get; set; }
        public int? JenisAtrLama { get; set; }
        public int? JenisAtrBaru { get; set; }
        public int? ProgressAtrLama { get; set; }
        public int? ProgressAtrBaru { get; set; }
        public sbyte IsPerdaPerpresLama { get; set; }
        public sbyte IsPerdaPerpresBaru { get; set; }
    }
}
