using Microsoft.EntityFrameworkCore;
using Protaru.Models;

namespace MonevAtr.Models
{
    public partial class PomeloDbContext : DbContext
    {
        public PomeloDbContext(DbContextOptions<PomeloDbContext> options) : base(options) { }

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
        public virtual DbSet<LogUser> LogUser { get; set; }
        public virtual DbSet<TugasUser> TugasUser { get; set; }
        public virtual DbSet<Saran> Saran { get; set; }

        public virtual DbSet<FilterPencarianRtr> FilterPencarianRtr { get; set; }
        public virtual DbSet<PencarianRtr> PencarianRtr { get; set; }
        public virtual DbSet<RdtrT5152Kabkota> RdtrT5152Kabkota { get; set; }
        public virtual DbSet<RdtrT5152Provinsi> RdtrT5152Provinsi { get; set; }
        public virtual DbSet<RtrwT5152Kabkota> RtrwT5152Kabkota { get; set; }
        public virtual DbSet<RtrwT5152Provinsi> RtrwT5152Provinsi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Atr>(entity =>
            {
                entity.HasKey(e => e.Kode)
                    .HasName("PRIMARY");

                entity.ToTable("atr");

                entity.HasIndex(e => e.KodeJenisAtr)
                    .HasName("FK_atr_jenis_atr");

                entity.HasIndex(e => e.KodeKabupatenKota)
                    .HasName("FK_atr_kabupaten_kota");

                entity.HasIndex(e => e.KodeKawasan)
                    .HasName("FK_atr_kawasan");

                entity.HasIndex(e => e.KodeProgressAtr)
                    .HasName("FK_atr_progress_atr");

                entity.HasIndex(e => e.KodeProvinsi)
                    .HasName("FK_atr_provinsi");

                entity.HasIndex(e => e.KodePulau)
                    .HasName("FK_atr_pulau");

                entity.HasIndex(e => e.NextRtr)
                    .HasName("FK_atr_atr_2");

                entity.HasIndex(e => e.PreviousRtr)
                    .HasName("FK_atr_atr");

                entity.Property(e => e.Kode).HasColumnType("int(11)");

                entity.Property(e => e.Aoi)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Keterangan)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.KodeJenisAtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeKawasan).HasColumnType("int(11)");

                entity.Property(e => e.KodeProgressAtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.KodePulau).HasColumnType("int(11)");

                entity.Property(e => e.Luas).HasColumnType("decimal(18,3)");

                entity.Property(e => e.Nama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NextRtr).HasColumnType("int(11)");

                entity.Property(e => e.Nomor)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PembaruanOleh)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PembaruanTerakhir)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Permasalahan)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PreviousRtr).HasColumnType("int(11)");

                entity.Property(e => e.StatusRevisi).HasColumnType("tinyint(4)");

                entity.Property(e => e.SudahDirevisi).HasColumnType("tinyint(4)");

                entity.Property(e => e.Tahun)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.TahunPenyusunan)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.Tanggal).HasColumnType("date");

                entity.Property(e => e.TindakLanjut)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL1FilePath)
                    .HasColumnName("TL1FilePath")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL1Keterangan)
                    .HasColumnName("TL1Keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL1Status)
                    .HasColumnName("TL1Status")
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL2FilePath)
                    .HasColumnName("TL2FilePath")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL2Keterangan)
                    .HasColumnName("TL2Keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL2Status)
                    .HasColumnName("TL2Status")
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL3FilePath)
                    .HasColumnName("TL3FilePath")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL3Keterangan)
                    .HasColumnName("TL3Keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL3Status)
                    .HasColumnName("TL3Status")
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL4FilePath)
                    .HasColumnName("TL4FilePath")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL4Keterangan)
                    .HasColumnName("TL4Keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL4Status)
                    .HasColumnName("TL4Status")
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL5FilePath)
                    .HasColumnName("TL5FilePath")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL5Keterangan)
                    .HasColumnName("TL5Keterangan")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TL5Status)
                    .HasColumnName("TL5Status")
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.JenisAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeJenisAtr)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_jenis_atr");

                entity.HasOne(d => d.KabupatenKota)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeKabupatenKota)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_kabupaten_kota");

                entity.HasOne(d => d.Kawasan)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeKawasan)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_kawasan");

                entity.HasOne(d => d.ProgressAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProgressAtr)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_progress_atr");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProvinsi)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_provinsi");

                entity.HasOne(d => d.Pulau)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodePulau)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_pulau");

                entity.HasOne(d => d.NextRtrNavigation)
                    .WithMany(p => p.InverseNextRtrNavigation)
                    .HasForeignKey(d => d.NextRtr)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_atr_2");

                entity.HasOne(d => d.PreviousRtrNavigation)
                    .WithMany(p => p.InversePreviousRtrNavigation)
                    .HasForeignKey(d => d.PreviousRtr)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_atr_atr");
            });

            modelBuilder.Entity<AtrDokumen>(entity =>
            {
                entity.HasKey(e => e.Kode)
                    .HasName("PRIMARY");

                entity.ToTable("atr_dokumen");

                entity.HasIndex(e => e.KodeAtr)
                    .HasName("FK_atr_dokumen_atr");

                entity.HasIndex(e => e.KodeDokumen)
                    .HasName("FK_atr_dokumen_dokumen");

                entity.Property(e => e.Kode).HasColumnType("int(11)");

                entity.Property(e => e.FilePath).HasColumnType("varchar(255)");

                entity.Property(e => e.Keterangan).HasColumnType("varchar(1000)");

                entity.Property(e => e.KodeAtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeDokumen).HasColumnType("int(11)");

                entity.Property(e => e.Nomor)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Tanggal)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'0000-00-00'");

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
                entity.HasKey(e => e.Kode)
                    .HasName("PRIMARY");

                entity.ToTable("kabupaten_kota");

                entity.HasIndex(e => e.KodeProvinsi)
                    .HasName("FK_kabupaten_kota_provinsi");

                entity.Property(e => e.Kode).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.Lat).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Long).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

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

                entity.Property(e => e.Lat).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Long).HasColumnType("decimal(9,6)");
            });

            modelBuilder.Entity<KawasanKabupatenKota>(entity =>
            {
                entity.HasKey(e => new { e.KodeKawasan, e.KodeKabupatenKota })
                    .HasName("PRIMARY");

                entity.ToTable("kawasan_kabupaten_kota");

                entity.HasIndex(e => e.KodeKabupatenKota)
                    .HasName("FK_kawasan_kabupaten_kota_kabupaten_kota");

                entity.Property(e => e.KodeKawasan).HasColumnType("int(11)");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

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
                entity.HasKey(e => e.Kode)
                    .HasName("PRIMARY");

                entity.ToTable("provinsi");

                // entity.HasIndex(e => e.KodeArea)
                //     .HasName("FK_provinsi_area");

                // entity.Property(e => e.KodeArea).HasDefaultValueSql("0");

                entity.Property(e => e.Nama)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Lat).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Long).HasColumnType("decimal(9,6)");

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

            modelBuilder.Entity<FilterPencarianRtr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("filter_pencarian_rtr");

                entity.Property(e => e.Kode).HasColumnType("int(11)");

                entity.Property(e => e.KodeDokumen).HasColumnType("int(11)");

                entity.Property(e => e.KodeFasilitasKegiatan)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeJenisRtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeProgressRtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsiKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.Nama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nomor)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusRevisi).HasColumnType("tinyint(4)");

                entity.Property(e => e.Tahun)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.TahunDokumen).HasColumnType("int(4)");
            });

            modelBuilder.Entity<PencarianRtr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("pencarian_rtr");

                entity.Property(e => e.BulanDokumen).HasColumnType("int(2)");

                entity.Property(e => e.Kode).HasColumnType("int(11)");

                entity.Property(e => e.KodeDokumen).HasColumnType("int(11)");

                entity.Property(e => e.KodeJenisRtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeProgressRtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsiKabupatenKota)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaJenisRtr)
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaKabupatenKota)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaProgressRtr)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaProvinsi)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaProvinsiKabupatenKota)
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nomor)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusRevisi).HasColumnType("tinyint(4)");

                entity.Property(e => e.Tahun)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.TahunDokumen).HasColumnType("int(4)");

                entity.Property(e => e.TanggalDokumen)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'0000-00-00'");
            });

            modelBuilder.Entity<RdtrT5152Kabkota>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("rdtr_t5152_kabkota");

                entity.Property(e => e.IsPerdaPerpresBaru).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsPerdaPerpresLama).HasColumnType("tinyint(4)");

                entity.Property(e => e.JenisAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.JenisAtrLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeBaru)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.NamaBaru)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaLama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProgressAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.ProgressAtrLama).HasColumnType("int(11)");
            });

            modelBuilder.Entity<RdtrT5152Provinsi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("rdtr_t5152_provinsi");

                entity.Property(e => e.IsPerdaPerpresBaru).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsPerdaPerpresLama).HasColumnType("tinyint(4)");

                entity.Property(e => e.JenisAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.JenisAtrLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeBaru)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.NamaBaru)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaLama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProgressAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.ProgressAtrLama).HasColumnType("int(11)");
            });

            modelBuilder.Entity<RtrwT5152Kabkota>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("rtrw_t5152_kabkota");

                entity.Property(e => e.IsPerdaPerpresBaru).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsPerdaPerpresLama).HasColumnType("tinyint(4)");

                entity.Property(e => e.JenisAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.JenisAtrLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeBaru)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.NamaBaru)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaLama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProgressAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.ProgressAtrLama).HasColumnType("int(11)");
            });

            modelBuilder.Entity<RtrwT5152Provinsi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("rtrw_t5152_provinsi");

                entity.Property(e => e.IsPerdaPerpresBaru).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsPerdaPerpresLama).HasColumnType("tinyint(4)");

                entity.Property(e => e.JenisAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.JenisAtrLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeBaru)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.KodeKabupatenKota).HasColumnType("int(11)");

                entity.Property(e => e.KodeLama).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.NamaBaru)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NamaLama)
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProgressAtrBaru).HasColumnType("int(11)");

                entity.Property(e => e.ProgressAtrLama).HasColumnType("int(11)");
            });


            modelBuilder.Entity<LogUser>(entity =>
            {
                entity.ToTable("log_user");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.JenisKegiatan).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Waktu)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Saran>(entity =>
            {
                entity.ToTable("saran");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Isi)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TugasUser>(entity =>
            {
                entity.ToTable("tugas_user");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.BatasWaktu)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'0000-00-00'");

                entity.Property(e => e.Jumlah).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}