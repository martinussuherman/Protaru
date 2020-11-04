using System;
using System.Collections.Generic;
using System.Linq;

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

        public static StatusRevisi Kosong => kosong;

        public static StatusRevisi RegularT51 => regularT51;

        public static StatusRevisi RegularT52 => regularT52;

        public static StatusRevisi RevisiT52 => revisiT52;

        public static StatusRevisi RevisiT53 => revisiT53;

        public static StatusRevisi RevisiT54 => revisiT54;

        public static string NamaStatusRevisiRegular(sbyte? kode)
        {
            return NamaStatusRevisiRegular((byte?)kode);
        }
        public static string NamaStatusRevisiRegular(byte? kode)
        {
            return !kode.HasValue || !listRegular.Exists(s => s.Kode == kode.Value) ?
                String.Empty :
                listRegular
                .Find(s => s.Kode == kode.Value)
                .Nama;
        }

        public static string NamaStatusRevisiRevisi(sbyte? kode)
        {
            return NamaStatusRevisiRevisi((byte?)kode);
        }
        public static string NamaStatusRevisiRevisi(byte? kode)
        {
            return !kode.HasValue || !listRevisi.Exists(s => s.Kode == kode.Value) ?
                String.Empty :
                listRevisi
                .Find(s => s.Kode == kode.Value)
                .Nama;
        }

        public static string NamaStatusRevisi(byte? kode)
        {
            if (kode.HasValue)
                return listAll.FirstOrDefault(e => e.Kode == kode).Nama;

            return String.Empty;

            // return !kode.HasValue || !listAll.Exists(s => s.Kode == kode.Value) ?
            //     String.Empty :
            //     listAll
            //         .Find(s => s.Kode == kode.Value)
            //         .Nama;
        }


        private static readonly StatusRevisi kosong = new StatusRevisi(0, String.Empty);

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

        private static readonly List<StatusRevisi> listAll = new List<StatusRevisi>
        {
            kosong,
            regularT51,
            regularT52,
            revisiT52,
            revisiT53,
            revisiT54
        };
    }
}