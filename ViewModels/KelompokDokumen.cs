using System;
using System.ComponentModel.DataAnnotations;
using MonevAtr.Models;

namespace MonevAtr.ViewModels
{
    public class KelompokDokumen
    {
        public Models.KelompokDokumen Data { get; set; } = new Models.KelompokDokumen();

        public int Kode
        {
            get => Data.Kode;
            set => Data.Kode = value;
        }

        [Required(ErrorMessage = "{0} harus diisi."), MaxLength(50, ErrorMessage = "{0} maksimal {1} karakter.")]
        public string Nama
        {
            get => Data.Nama;
            set => Data.Nama = value;
        }

        [Required(ErrorMessage = "Nomor Urut Kelompok Dokumen harus diisi."), Range(1, Int32.MaxValue, ErrorMessage = "Nomor Urut Kelompok Dokumen harus > 0.")]
        public int Nomor { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Jenis ATR harus diisi."), Display(Name = "Jenis ATR")]
        public int KodeJenis { get; set; }

        [Display(Name = "Jenis ATR")]
        public JenisAtr Jenis { get; set; }

        public string NamaDanNamaJenis
        {
            get
            {
                if (Jenis != null)
                {
                    return Nama + " - " + Jenis.Nama;
                }

                return Nama;
            }
        }
    }
}