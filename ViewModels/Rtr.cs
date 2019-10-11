using MonevAtr.Models;

namespace MonevAtr.ViewModels
{
    public class Rtr
    {
        public Atr Data { get; set; } = new Atr();

        public int Kode
        {
            get => Data.Kode;
            set => Data.Kode = value;
        }

        public string Nama
        {
            get => Data.Nama;
            set => Data.Nama = value;
        }
    }
}