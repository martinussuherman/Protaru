using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public partial class MonevAtrDbContext : DbContext
    {
        public MonevAtrDbContext(DbContextOptions<MonevAtrDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Atr> Atr { get; set; }
        public virtual DbSet<AtrDokumen> AtrDokumen { get; set; }
        public virtual DbSet<AtrDokumenTindakLanjut> AtrDokumenTindakLanjut { get; set; }
        public virtual DbSet<AtrProgressInfo> AtrProgressInfo { get; set; }
        public virtual DbSet<Dokumen> Dokumen { get; set; }
        public virtual DbSet<FasilitasKegiatan> FasilitasKegiatan { get; set; }
        public virtual DbSet<JenisAtr> JenisAtr { get; set; }
        public virtual DbSet<KabupatenKota> KabupatenKota { get; set; }
        public virtual DbSet<Kawasan> Kawasan { get; set; }
        public virtual DbSet<KawasanKabupatenKota> KawasanKabupatenKota { get; set; }
        public virtual DbSet<KawasanProvinsi> KawasanProvinsi { get; set; }
        public virtual DbSet<KelompokDokumen> KelompokDokumen { get; set; }
        public virtual DbSet<ProgressAtr> ProgressAtr { get; set; }
        public virtual DbSet<Provinsi> Provinsi { get; set; }

        public virtual DbSet<Pulau> Pulau { get; set; }

        public virtual DbSet<RtrFasilitasKegiatan> RtrFasilitasKegiatan { get; set; }
        // public virtual DbSet<StatusRevisi> StatusRevisi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Atr>(entity =>
            {
                entity.HasIndex(e => e.KodeJenisAtr)
                    .HasName("FK_atr_jenis_atr");

                entity.HasIndex(e => e.KodeKabupatenKota)
                    .HasName("FK_atr_kabupaten_kota");

                entity.HasIndex(e => e.KodeProgressAtr)
                    .HasName("FK_atr_progress_atr");

                entity.HasIndex(e => e.KodeProvinsi)
                    .HasName("FK_atr_provinsi");

                entity.Property(e => e.Aoi)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.KodeJenisAtr).HasDefaultValueSql("NULL");

                entity.Property(e => e.KodeKabupatenKota).HasDefaultValueSql("NULL");

                entity.Property(e => e.KodeProgressAtr).HasDefaultValueSql("NULL");

                entity.Property(e => e.KodeProvinsi).HasDefaultValueSql("NULL");

                entity.Property(e => e.Luas).HasDefaultValueSql("0.000");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PembaruanOleh)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PembaruanTerakhir)
                    .HasDefaultValueSql("current_timestamp()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Permasalahan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.StatusRevisi).HasDefaultValueSql("NULL");

                entity.Property(e => e.SudahDirevisi).HasDefaultValueSql("0");

                entity.Property(e => e.Tahun).HasDefaultValueSql("0000");

                entity.Property(e => e.TahunPenyusunan).HasDefaultValueSql("0000");

                entity.Property(e => e.Tanggal).HasDefaultValueSql("NULL");

                entity.Property(e => e.TindakLanjut)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL1FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL1Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL1Status).HasDefaultValueSql("NULL");

                entity.Property(e => e.TL2FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL2Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL2Status).HasDefaultValueSql("NULL");

                entity.Property(e => e.TL3FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL3Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL3Status).HasDefaultValueSql("NULL");

                entity.Property(e => e.TL4FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL4Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL4Status).HasDefaultValueSql("NULL");

                entity.Property(e => e.TL5FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL5Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TL5Status).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.JenisAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeJenisAtr)
                    .HasConstraintName("FK_atr_jenis_atr");

                entity.HasOne(d => d.KabupatenKota)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeKabupatenKota)
                    .HasConstraintName("FK_atr_kabupaten_kota");

                entity.HasOne(d => d.ProgressAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProgressAtr)
                    .HasConstraintName("FK_atr_progress_atr");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProvinsi)
                    .HasConstraintName("FK_atr_provinsi");
            });

            modelBuilder.Entity<AtrDokumen>(entity =>
            {
                entity.HasIndex(e => e.KodeAtr)
                    .HasName("FK_atr_dokumen_atr");

                entity.HasIndex(e => e.KodeDokumen)
                    .HasName("FK_atr_dokumen_dokumen");

                entity.Property(e => e.FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Nomor)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Status).HasDefaultValueSql("''");

                entity.Property(e => e.Tanggal).HasDefaultValueSql("'0000-00-00'");

                entity.HasOne(d => d.Atr)
                    .WithMany(p => p.AtrDokumen)
                    .HasForeignKey(d => d.KodeAtr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_atr_dokumen_atr");

                entity.HasOne(d => d.Dokumen)
                    .WithMany(p => p.AtrDokumen)
                    .HasForeignKey(d => d.KodeDokumen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_atr_dokumen_dokumen");
            });

            modelBuilder.Entity<AtrDokumenTindakLanjut>(entity =>
            {
                entity.HasIndex(e => e.KodeAtr)
                    .HasName("FK_atr_dokumen_tindak_lanjut_atr");

                entity.HasIndex(e => e.KodeDokumen)
                    .HasName("FK_atr_dokumen_tindak_lanjut_dokumen");

                entity.Property(e => e.FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Nomor).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Rtr)
                    .WithMany(p => p.DokumenTindakLanjut)
                    .HasForeignKey(d => d.KodeAtr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_atr_dokumen_tindak_lanjut_atr");

                entity.HasOne(d => d.Dokumen)
                    .WithMany(p => p.AtrDokumenTindakLanjut)
                    .HasForeignKey(d => d.KodeDokumen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_atr_dokumen_tindak_lanjut_dokumen");
            });

            modelBuilder.Entity<AtrProgressInfo>(entity =>
            {
                entity.HasIndex(e => e.KodeAtr)
                    .HasName("FK_atr_progress_info_atr");

                entity.HasIndex(e => e.KodeProgressAtr)
                    .HasName("FK_atr_progress_info_progress_atr");

                entity.Property(e => e.FilePath)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Permasalahan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.TindakLanjut)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                // entity.HasOne(d => d.KodeAtrNavigation)
                //     .WithMany(p => p.AtrProgressInfo)
                //     .HasForeignKey(d => d.KodeAtr)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_atr_progress_info_atr");

                // entity.HasOne(d => d.KodeProgressAtrNavigation)
                //     .WithMany(p => p.AtrProgressInfo)
                //     .HasForeignKey(d => d.KodeProgressAtr)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_atr_progress_info_progress_atr");
            });

            modelBuilder.Entity<Dokumen>(entity =>
            {
                entity.HasIndex(e => e.KodeKelompokDokumen)
                    .HasName("FK_dokumen_kelompok_dokumen");

                entity.Property(e => e.AmbilNomor).HasDefaultValueSql("0");

                entity.Property(e => e.JumlahTindakLanjut).HasDefaultValueSql("0");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor).HasDefaultValueSql("0");

                entity.Property(e => e.UntukPublik).HasDefaultValueSql("0");

                entity.HasOne(d => d.KelompokDokumen)
                    .WithMany(p => p.Dokumen)
                    .HasForeignKey(d => d.KodeKelompokDokumen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dokumen_kelompok_dokumen");
            });

            modelBuilder.Entity<FasilitasKegiatan>(entity =>
            {
                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<JenisAtr>(entity =>
            {
                // entity.Property(e => e.LastModified).HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor).HasDefaultValueSql("NULL");
            });

            modelBuilder.Entity<KabupatenKota>(entity =>
            {
                entity.HasIndex(e => e.KodeProvinsi)
                    .HasName("FK_kabupaten_kota_provinsi");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.KabupatenKota)
                    .HasForeignKey(d => d.KodeProvinsi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kabupaten_kota_provinsi");
            });

            modelBuilder.Entity<Kawasan>(entity =>
            {
                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<KawasanKabupatenKota>(entity =>
            {
                entity.HasKey(e => new { e.KodeKawasan, e.KodeKabupatenKota });

                entity.HasIndex(e => e.KodeKabupatenKota)
                    .HasName("FK_kawasan_kabupaten_kota_kabupaten_kota");

                entity.HasOne(d => d.KabupatenKota)
                    .WithMany(p => p.KawasanKabupatenKota)
                    .HasForeignKey(d => d.KodeKabupatenKota)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kawasan_kabupaten_kota_kabupaten_kota");

                entity.HasOne(d => d.Kawasan)
                    .WithMany(p => p.KawasanKabupatenKota)
                    .HasForeignKey(d => d.KodeKawasan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kawasan_kabupaten_kota_kawasan");
            });

            modelBuilder.Entity<KawasanProvinsi>(entity =>
            {
                entity.HasKey(e => new { e.KodeKawasan, e.KodeProvinsi });

                entity.HasIndex(e => e.KodeProvinsi)
                    .HasName("FK_kawasan_provinsi_provinsi");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.KawasanProvinsi)
                    .HasForeignKey(d => d.KodeProvinsi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kawasan_provinsi_provinsi");

                entity.HasOne(d => d.Kawasan)
                    .WithMany(p => p.KawasanProvinsi)
                    .HasForeignKey(d => d.KodeKawasan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kawasan_provinsi_kawasan");
            });

            modelBuilder.Entity<KelompokDokumen>(entity =>
            {
                entity.HasIndex(e => e.KodeJenisAtr)
                    .HasName("FK_kelompok_dokumen_jenis_atr");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor).HasDefaultValueSql("0");

                entity.HasOne(d => d.JenisAtr)
                    .WithMany(p => p.KelompokDokumen)
                    .HasForeignKey(d => d.KodeJenisAtr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kelompok_dokumen_jenis_atr");
            });

            modelBuilder.Entity<ProgressAtr>(entity =>
            {
                entity.HasIndex(e => e.KodeJenisAtr)
                    .HasName("FK_progress_atr_jenis_atr");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor).HasDefaultValueSql("0");

                entity.HasOne(d => d.JenisAtr)
                    .WithMany(p => p.ProgressAtr)
                    .HasForeignKey(d => d.KodeJenisAtr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_progress_atr_jenis_atr");
            });

            modelBuilder.Entity<Provinsi>(entity =>
            {
                // entity.HasIndex(e => e.KodeArea)
                //     .HasName("FK_provinsi_area");

                // entity.Property(e => e.KodeArea).HasDefaultValueSql("0");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                // entity.HasOne(d => d.KodeAreaNavigation)
                //     .WithMany(p => p.Provinsi)
                //     .HasForeignKey(d => d.KodeArea)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_provinsi_area");
            });

            modelBuilder.Entity<RtrFasilitasKegiatan>(entity =>
            {
                entity.HasIndex(e => e.KodeFasilitasKegiatan)
                    .HasName("FK_rtr_fasilitas_kegiatan_fasilitas_kegiatan");

                entity.HasIndex(e => e.KodeRtr)
                    .HasName("FK_rtr_fasilitas_kegiatan_atr");

                entity.Property(e => e.Keterangan)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.KodeFasilitasKegiatan).HasDefaultValueSql("0");

                entity.Property(e => e.KodeRtr).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.Tahun).HasDefaultValueSql("0000");

                entity.HasOne(d => d.FasilitasKegiatan)
                    .WithMany(p => p.RtrFasilitasKegiatan)
                    .HasForeignKey(d => d.KodeFasilitasKegiatan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rtr_fasilitas_kegiatan_fasilitas_kegiatan");

                entity.HasOne(d => d.Rtr)
                    .WithMany(p => p.RtrFasilitasKegiatan)
                    .HasForeignKey(d => d.KodeRtr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rtr_fasilitas_kegiatan_atr");
            });

            modelBuilder.Entity<PencarianRtr>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<FilterPencarianRtr>(entity =>
            {
                entity.HasNoKey();
            });

            // modelBuilder.Entity<StatusRevisi>(entity =>
            // {
            //     entity.HasIndex(e => e.KodeJenis)
            //         .HasName("FK_status_revisi_jenis_atr");

            //     entity.Property(e => e.Nama).IsUnicode(false);

            //     entity.HasOne(d => d.KodeJenisNavigation)
            //         .WithMany(p => p.StatusRevisi)
            //         .HasForeignKey(d => d.KodeJenis)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_status_revisi_jenis_atr");
            // });
        }
    }
}