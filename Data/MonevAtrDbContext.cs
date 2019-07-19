using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class MonevAtrDbContext : DbContext
    {
        public MonevAtrDbContext(DbContextOptions<MonevAtrDbContext> options) : base(options) {}

        public DbSet<Provinsi> Provinsi { get; set; }

        public DbSet<KabupatenKota> KabupatenKota { get; set; }

        public DbSet<JenisAtr> JenisAtr { get; set; }

        public DbSet<ProgressAtr> ProgressAtr { get; set; }

        public DbSet<KelompokDokumen> KelompokDokumen { get; set; }

        public DbSet<Dokumen> Dokumen { get; set; }

        public DbSet<AtrDokumen> AtrDokumen { get; set; }

        public DbSet<Atr> Atr { get; set; }
    }
}