using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public static SelectList SelectListStatusRevisiRtrwRegular
        {
            get
            {
                List<StatusRevisi> list = new List<StatusRevisi>();
                list.Add(new StatusRevisi(0, "Pilih Status RTRW T5-1"));
                list.Add(new StatusRevisi(1, "T5-1"));
                list.Add(new StatusRevisi(2, "T5-2"));

                return new SelectList(list, "Kode", "Nama");
            }
        }

        public static SelectList SelectListStatusRevisiRtrwRevisi
        {
            get
            {
                List<StatusRevisi> list = new List<StatusRevisi>();
                list.Add(new StatusRevisi(0, "Pilih Status RTRW T5-2"));
                list.Add(new StatusRevisi(4, "T5-2"));
                list.Add(new StatusRevisi(5, "T5-3"));
                list.Add(new StatusRevisi(6, "T5-4"));

                return new SelectList(list, "Kode", "Nama");
            }
        }
    }
}