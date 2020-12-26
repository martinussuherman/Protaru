using System;
using System.Collections.Generic;

namespace MonevAtr.Models
{
    public partial class Atr : IKode
    {
        public Atr()
        {
            AtrDokumen = new HashSet<AtrDokumen>();
            DokumenTindakLanjut = new HashSet<AtrDokumenTindakLanjut>();
            AtrProgressInfo = new HashSet<AtrProgressInfo>();
            InverseNextRtrNavigation = new HashSet<Atr>();
            InversePreviousRtrNavigation = new HashSet<Atr>();
            RtrFasilitasKegiatan = new HashSet<RtrFasilitasKegiatan>();
        }

        public int Kode { get; set; }
        public string Nama { get; set; }
        public int? KodeProvinsi { get; set; }
        public int? KodeKabupatenKota { get; set; }
        public int? KodePulau { get; set; }
        public int? KodeKawasan { get; set; }
        public string Nomor { get; set; }
        public int? KodeJenisAtr { get; set; }
        public DateTime? Tanggal { get; set; }
        public int? KodeProgressAtr { get; set; }
        public string Aoi { get; set; }
        public decimal Luas { get; set; }
        public short Tahun { get; set; }
        public short TahunPenyusunan { get; set; }
        public sbyte? StatusRevisi { get; set; }
        public string Permasalahan { get; set; }
        public string TindakLanjut { get; set; }
        public string Keterangan { get; set; }
        public sbyte SudahDirevisi { get; set; }
        public string TL1Status { get; set; }
        public string TL1Keterangan { get; set; }
        public string TL1FilePath { get; set; }
        public string TL2Status { get; set; }
        public string TL2Keterangan { get; set; }
        public string TL2FilePath { get; set; }
        public string TL3Status { get; set; }
        public string TL3Keterangan { get; set; }
        public string TL3FilePath { get; set; }
        public string TL4Status { get; set; }
        public string TL4Keterangan { get; set; }
        public string TL4FilePath { get; set; }
        public string TL5Status { get; set; }
        public string TL5Keterangan { get; set; }
        public string TL5FilePath { get; set; }
        public DateTime PembaruanTerakhir { get; set; }
        public string PembaruanOleh { get; set; }
        public int? PreviousRtr { get; set; }
        public int? NextRtr { get; set; }

        public virtual JenisAtr JenisAtr { get; set; }
        public virtual KabupatenKota KabupatenKota { get; set; }
        public virtual Kawasan Kawasan { get; set; }
        public virtual ProgressAtr ProgressAtr { get; set; }
        public virtual Provinsi Provinsi { get; set; }
        public virtual Pulau Pulau { get; set; }
        public virtual Atr NextRtrNavigation { get; set; }
        public virtual Atr PreviousRtrNavigation { get; set; }
        public virtual ICollection<AtrDokumen> AtrDokumen { get; set; }
        public virtual ICollection<AtrDokumenTindakLanjut> DokumenTindakLanjut { get; set; }
        public virtual ICollection<AtrProgressInfo> AtrProgressInfo { get; set; }
        public virtual ICollection<Atr> InverseNextRtrNavigation { get; set; }
        public virtual ICollection<Atr> InversePreviousRtrNavigation { get; set; }
        public virtual ICollection<RtrFasilitasKegiatan> RtrFasilitasKegiatan { get; set; }
    }
}
