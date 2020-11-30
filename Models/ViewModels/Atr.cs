using System.ComponentModel.DataAnnotations.Schema;

namespace MonevAtr.Models
{
    public partial class Atr
    {
        [NotMapped]
        public string DisplayNamaPulau =>
            Pulau != null ? Pulau.Nama : string.Empty;

        [NotMapped]
        public string DisplayNamaKawasan =>
            Kawasan != null ? Kawasan.Nama : string.Empty;

        [NotMapped]
        public string DisplayNamaProgress =>
            ProgressAtr != null ? ProgressAtr.Nama : string.Empty;

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

                return string.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaKabupatenKota =>
            KabupatenKota != null ? KabupatenKota.Nama : string.Empty;

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

                return string.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaStatusRevisi =>
            Models.StatusRevisi.NamaStatusRevisi(StatusRevisi);

        [NotMapped]
        public JenisRtrEnum DisplayJenisRtr => (JenisRtrEnum)KodeJenisAtr;

        [NotMapped]
        public string DisplayJenisRtrShort
        {
            get
            {
                JenisRtrEnum jenis = (JenisRtrEnum)KodeJenisAtr;

                if (jenis == JenisRtrEnum.RdtrT51 || jenis == JenisRtrEnum.RdtrT52)
                {
                    return "RDTR";
                }

                if (jenis == JenisRtrEnum.RtrwT50 || jenis == JenisRtrEnum.RtrwT51 || jenis == JenisRtrEnum.RtrwT52)
                {
                    return "RTRW";
                }

                if (jenis == JenisRtrEnum.RtrwnT51 || jenis == JenisRtrEnum.RtrwnT52)
                {
                    return "RTRWN";
                }

                if (jenis == JenisRtrEnum.RtrKpnT51 || jenis == JenisRtrEnum.RtrKpnT52)
                {
                    return "KPN";
                }

                if (jenis == JenisRtrEnum.RtrKsnT51 || jenis == JenisRtrEnum.RtrKsnT52)
                {
                    return "KSN";
                }

                if (jenis == JenisRtrEnum.RtrPulauT51 || jenis == JenisRtrEnum.RtrPulauT52)
                {
                    return "PULAU";
                }

                return string.Empty;
            }
        }

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
            return !string.IsNullOrEmpty(status) && status == "1";
        }
    }
}