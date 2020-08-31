using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    public partial class Atr
    {
        [NotMapped]
        public string DisplayNamaPulau =>
            Pulau != null ? Pulau.Nama : String.Empty;

        [NotMapped]
        public string DisplayNamaKawasan =>
            Kawasan != null ? Kawasan.Nama : String.Empty;

        [NotMapped]
        public string DisplayNamaProgress =>
            ProgressAtr != null ? ProgressAtr.Nama : String.Empty;

        [NotMapped]
        public string DisplayNamaProvinsi
        {
            get
            {
                if (Provinsi != null)
                {
                    return Provinsi.Nama;
                }

                if (KabupatenKota != null)
                {
                    return KabupatenKota.Provinsi.Nama;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaKabupatenKota =>
            KabupatenKota != null ? KabupatenKota.Nama : String.Empty;

        [NotMapped]
        public string DisplayNamaProvinsiKabupatenKota
        {
            get
            {
                if (KabupatenKota != null)
                {
                    return KabupatenKota.Provinsi.Nama + ", " + KabupatenKota.Nama;
                }

                if (Provinsi != null)
                {
                    return Provinsi.Nama;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaStatusRevisi =>
            Models.StatusRevisi.NamaStatusRevisi(StatusRevisi);

        [NotMapped]
        public JenisRtrEnum DisplayJenisRtr => (JenisRtrEnum)JenisAtr.Kode;

        [NotMapped]
        public bool TL1StatusYes
        {
            get => IsStatusYes(TL1Status);
            set => TL1Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL2StatusYes
        {
            get => IsStatusYes(TL2Status);
            set => TL2Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL3StatusYes
        {
            get => IsStatusYes(TL3Status);
            set => TL3Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL4StatusYes
        {
            get => IsStatusYes(TL4Status);
            set => TL4Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL5StatusYes
        {
            get => IsStatusYes(TL5Status);
            set => TL5Status = ConvertToStatusString(value);
        }

        private string ConvertToStatusString(bool status)
        {
            return status ? "1" : "0";
        }

        private bool IsStatusYes(string status)
        {
            return !String.IsNullOrEmpty(status) && status == "1";
        }
    }
}