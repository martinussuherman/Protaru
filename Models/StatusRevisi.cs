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

        public static SelectList SelectListStatusRevisi
        {
            get
            {
                List<StatusRevisi> list = new List<StatusRevisi>();
                list.Add(new StatusRevisi(0, "Pilih Status Revisi RTRW Regular"));
                list.Add(new StatusRevisi(1, "T5-1"));
                list.Add(new StatusRevisi(2, "Sedang PK"));
                list.Add(new StatusRevisi(3, "Revisi"));

                return new SelectList(list, "Kode", "Nama");
            }
        }
    }
}