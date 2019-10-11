using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string DisplayNamaProvinsi
        {
            get
            {
                if (this.Provinsi != null)
                {
                    return this.Provinsi.Nama;
                }

                if (this.KabupatenKota != null)
                {
                    return this.KabupatenKota.Provinsi.Nama;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaKabupatenKota
        {
            get
            {
                return this.KabupatenKota != null ? this.KabupatenKota.Nama : String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaProvinsiKabupatenKota
        {
            get
            {
                if (this.KabupatenKota != null)
                {
                    return this.KabupatenKota.Provinsi.Nama + ", " + this.KabupatenKota.Nama;
                }

                if (this.Provinsi != null)
                {
                    return this.Provinsi.Nama;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public bool TL1StatusYes
        {
            get => IsStatusYes(this.TL1Status);
            set => this.TL1Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL2StatusYes
        {
            get => IsStatusYes(this.TL2Status);
            set => this.TL2Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL3StatusYes
        {
            get => IsStatusYes(this.TL3Status);
            set => this.TL3Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL4StatusYes
        {
            get => IsStatusYes(this.TL4Status);
            set => this.TL4Status = ConvertToStatusString(value);
        }

        [NotMapped]
        public bool TL5StatusYes
        {
            get => IsStatusYes(this.TL5Status);
            set => this.TL5Status = ConvertToStatusString(value);
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