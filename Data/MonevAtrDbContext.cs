using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Models
{
    public class MonevAtrDbContext : DbContext
    {
        public MonevAtrDbContext(DbContextOptions<MonevAtrDbContext> options)
            : base(options)
        {
        }

        public SelectList SelectListProvinsi
        {
            get
            {
                IList<Provinsi> list = this.Provinsi.ToList();
                list.Insert(0, new Provinsi(0, "Pilih Provinsi"));
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList SelectListKabupatenKota
        {
            get
            {
                IList<KabupatenKota> list = this.KabupatenKota.ToList();
                InsertPilihKabupatenKota(list);
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList EmptySelectListKabupatenKota
        {
            get
            {
                IList<KabupatenKota> list = new List<KabupatenKota>();
                InsertPilihKabupatenKota(list);
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList SelectListJenisAtr
        {
            get
            {
                IList<JenisAtr> list = this.JenisAtr.ToList();
                list.Insert(0, new JenisAtr(0, "Pilih Jenis ATR"));
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public SelectList SelectListKelompokDokumen
        {
            get
            {
                IList<KelompokDokumen> list = this.KelompokDokumen.ToList();
                list.Insert(0, new KelompokDokumen(0, "Pilih Kelompok Dokumen"));
                return new SelectList(list, "Kode", "Nama");
            }
        }

        public DbSet<Provinsi> Provinsi { get; set; }

        public DbSet<KabupatenKota> KabupatenKota { get; set; }

        public DbSet<JenisAtr> JenisAtr { get; set; }

        public DbSet<ProgressAtr> ProgressAtr { get; set; }

        public DbSet<KelompokDokumen> KelompokDokumen { get; set; }

        public DbSet<Dokumen> Dokumen { get; set; }

        public DbSet<AtrDokumen> AtrDokumen { get; set; }

        public DbSet<Atr> Atr { get; set; }

        private void InsertPilihKabupatenKota(IList<KabupatenKota> list)
        {
            list.Insert(0, new KabupatenKota(0, "Pilih Kabupaten/Kota"));
        }
    }
}