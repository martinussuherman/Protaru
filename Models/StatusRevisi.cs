using System;
using System.Collections.Generic;

namespace MonevAtr.Models
{
    public class StatusRevisi
    {
        public StatusRevisi(int kode, string nama)
        {
            this.Kode = kode;
            this.Nama = nama;
        }

        public int Kode { get; set; }

        public string Nama { get; set; }

        public static StatusRevisi RegularT51
        {
            get
            {
                return regularT51;
            }
        }

        public static StatusRevisi RegularT52
        {
            get
            {
                return regularT52;
            }
        }

        public static StatusRevisi RevisiT52
        {
            get
            {
                return revisiT52;
            }
        }

        public static StatusRevisi RevisiT53
        {
            get
            {
                return revisiT53;
            }
        }

        public static StatusRevisi RevisiT54
        {
            get
            {
                return revisiT54;
            }
        }

        public static string NamaStatusRevisiRegular(byte? kode)
        {
            if (!kode.HasValue)
            {
                return String.Empty;
            }

            return listRegular
                .Find(s => s.Kode == kode.Value)
                .Nama;
        }

        public static string NamaStatusRevisiRevisi(byte? kode)
        {
            if (!kode.HasValue)
            {
                return String.Empty;
            }

            return listRevisi
                .Find(s => s.Kode == kode.Value)
                .Nama;
        }

        private static readonly StatusRevisi regularT51 = new StatusRevisi(1, "T5-1");

        private static readonly StatusRevisi regularT52 = new StatusRevisi(2, "T5-2");

        private static readonly StatusRevisi revisiT52 = new StatusRevisi(4, "T5-2");

        private static readonly StatusRevisi revisiT53 = new StatusRevisi(5, "T5-3");

        private static readonly StatusRevisi revisiT54 = new StatusRevisi(6, "T5-4");

        private static readonly List<StatusRevisi> listRegular = new List<StatusRevisi>
        {
            regularT51,
            regularT52
        };

        private static readonly List<StatusRevisi> listRevisi = new List<StatusRevisi>
        {
            revisiT52,
            revisiT53,
            revisiT54
        };
    }
}