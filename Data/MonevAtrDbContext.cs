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
                .OrderBy(p => p.Nama)
                .ToListAsync();

            list.Insert(0, new Provinsi(0, "Pilih Provinsi"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListKabupatenKota()
        {
            IList<KabupatenKota> list = await this.KabupatenKota
                .OrderBy(k => k.Nama)
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
                .OrderBy(k => k.KodeJenisAtr)
                .ThenBy(k => k.Nomor)
                .ToListAsync();

            list.Insert(0, new KelompokDokumen(0, "Pilih Kelompok Dokumen"));
            return new SelectList(list, "Kode", "DisplayNamaJenisAtr");
        }

        public async Task<SelectList> GetSelectListProgressRdtr()
        {
            IList<ProgressAtr> list = await this.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int) JenisAtrEnum.RdtrPerda)
                .OrderBy(p => p.Nomor)
                .ToListAsync();

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RDTR"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListProgressRtrwRegular()
        {
            IList<ProgressAtr> list = await this.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int) JenisAtrEnum.RtrwRegular)
                .OrderBy(p => p.Nomor)
                .ToListAsync();

            list.Insert(0, new ProgressAtr(0, "Pilih Progress RTRW T5-1"));
            return new SelectList(list, "Kode", "Nama");
        }

        public async Task<SelectList> GetSelectListProgressRtrwRevisi()
        {
            IList<ProgressAtr> list = await this.ProgressAtr
                .Where(p => p.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi)
                .OrderBy(p => p.Nomor)
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