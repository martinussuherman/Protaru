using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protaru.Models
{
    public partial class PencarianRtr
    {
        [NotMapped]
        public string DetailPath { get; set; }

        [NotMapped]
        public string NamaStatusRevisi { get; set; }

        [NotMapped]
        public string DisplayNama => Nama ?? String.Empty;

        [NotMapped]
        public string DisplayNamaProvinsi
        {
            get
            {
                if (KodeProvinsi != null)
                {
                    return NamaProvinsi;
                }

                if (KodeKabupatenKota != null)
                {
                    return NamaProvinsiKabupatenKota;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayNamaProvinsiKabupatenKota
        {
            get
            {
                if (KodeKabupatenKota != null)
                {
                    return "Kab/Kota " + NamaKabupatenKota + ", Provinsi " + NamaProvinsiKabupatenKota;
                }

                if (KodeProvinsi != null)
                {
                    return "Provinsi " + NamaProvinsi;
                }

                return String.Empty;
            }
        }

        [NotMapped]
        public string DisplayTanggalDokumen => !TanggalDokumen.HasValue ?
            String.Empty :
            TanggalDokumen.Value.ToString("dd-MM-yyyy");
    }
}