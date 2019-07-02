using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class MonevAtrDbContext : DbContext
    {
        public MonevAtrDbContext(DbContextOptions<MonevAtrDbContext> options) : base(options) {}

        public SelectList EmptySelectListKabupatenKota
        {
            get
            {
                IList<KabupatenKota> list = new List<KabupatenKota>();
                InsertPilihKabupatenKota(list);
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

        public async Task<SelectList> GetSelectListProvinsi()
        {
            IList<Provinsi> list = await this.Provinsi
                .ToListAsync();
            list.Insert(0, new Provinsi(0, "Pilih Provinsi"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListKabupatenKota()
        {
            IList<KabupatenKota> list = await this.KabupatenKota
                .ToListAsync();
            InsertPilihKabupatenKota(list);
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListJenisAtr()
        {
            IList<JenisAtr> list = await this.JenisAtr
                .ToListAsync();
            list.Insert(0, new JenisAtr(0, "Pilih Jenis ATR"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListKelompokDokumen()
        {
            IList<KelompokDokumen> list = await this.KelompokDokumen
                .Include(k => k.JenisAtr)
                .ToListAsync();
            list.Insert(0, new KelompokDokumen(0, "Pilih Kelompok Dokumen"));
            return new SelectList(list, "Kode", "DisplayNamaJenisAtr");
        }

        public async Task<SelectList> GetSelectListProgressRdtr()
        {
            IList<ProgressAtr> list = await (from p in this.ProgressAtr where p.KodeJenisAtr == (int) JenisAtrEnum.RdtrPerda orderby p.Nomor select p)
                .ToListAsync();
            list.Insert(0, new ProgressAtr(0, "Pilih Progress RDTR"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListProgressRtrwRegular()
        {
            IList<ProgressAtr> list = await (from p in this.ProgressAtr where p.KodeJenisAtr == (int) JenisAtrEnum.RtrwRegular orderby p.Nomor select p)
                .ToListAsync();
            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListProgressRtrwRevisi()
        {
            IList<ProgressAtr> list = await (from p in this.ProgressAtr where p.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi orderby p.Nomor select p)
                .ToListAsync();
            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-2"));
            return new SelectList(list, "Kode", "Nama");
        }

        private void InsertPilihKabupatenKota(IList<KabupatenKota> list)
        {
            list.Insert(0, new KabupatenKota(0, "Pilih Kabupaten/Kota"));
        }
    }
}