using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<KawasanKabupatenKota> KawasanKabupatenKota { get; set; }
        public virtual DbSet<KawasanProvinsi> KawasanProvinsi { get; set; }
        public virtual DbSet<KelompokDokumen> KelompokDokumen { get; set; }
        public virtual DbSet<ProgressAtr> ProgressAtr { get; set; }
        public virtual DbSet<Provinsi> Provinsi { get; set; }
        public virtual DbSet<RtrFasilitasKegiatan> RtrFasilitasKegiatan { get; set; }

        public DbSet<PencarianRtr> PencarianRtr { get; set; }

        public DbSet<FilterPencarianRtr> FilterPencarianRtr { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.Kode)
                    .HasColumnType("int(11)");

                entity.Property(e => e.Aoi)
                    .HasColumnType("varchar(2000)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Keterangan)
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.KodeJenisAtr)
                    .HasColumnType("int(11)");

                entity.Property(e => e.KodeKabupatenKota)
                    .HasColumnType("int(11)");

                entity.Property(e => e.KodeKawasan).HasColumnType("int(11)");

                entity.Property(e => e.KodeProgressAtr).HasColumnType("int(11)");

                entity.Property(e => e.KodeProvinsi).HasColumnType("int(11)");

                entity.Property(e => e.KodePulau).HasColumnType("int(11)");

                entity.Property(e => e.Luas)
                    .HasColumnType("decimal(18,3)")
                    .HasDefaultValueSql("'0.000'");

                entity.Property(e => e.Nama)
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nomor)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PembaruanOleh)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PembaruanTerakhir)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'current_timestamp()'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Permasalahan).HasColumnType("varchar(2000)");

                entity.Property(e => e.StatusRevisi).HasColumnType("tinyint(4)");

                entity.Property(e => e.SudahDirevisi)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Tahun)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("0000");

                entity.Property(e => e.TahunPenyusunan)
                    .HasColumnType("year(4)")
                    .HasDefaultValueSql("0000");

                entity.Property(e => e.Tanggal).HasColumnType("date");

                entity.Property(e => e.TindakLanjut).HasColumnType("varchar(2000)");

                entity.Property(e => e.TL1FilePath)
                    .HasColumnName("TL1FilePath")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TL1Keterangan)
                    .HasColumnName("TL1Keterangan")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.TL1Status)
                    .HasColumnName("TL1Status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.TL2FilePath)
                    .HasColumnName("TL2FilePath")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TL2Keterangan)
                    .HasColumnName("TL2Keterangan")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.TL2Status)
                    .HasColumnName("TL2Status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.TL3FilePath)
                    .HasColumnName("TL3FilePath")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TL3Keterangan)
                    .HasColumnName("TL3Keterangan")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.TL3Status)
                    .HasColumnName("TL3Status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.TL4FilePath)
                    .HasColumnName("TL4FilePath")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TL4Keterangan)
                    .HasColumnName("TL4Keterangan")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.TL4Status)
                    .HasColumnName("TL4Status")
                    .HasColumnType("char(1)");

                entity.Property(e => e.TL5FilePath)
                    .HasColumnName("TL5FilePath")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TL5Keterangan)
                    .HasColumnName("TL5Keterangan")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.TL5Status)
                    .HasColumnName("TL5Status")
                    .HasColumnType("char(1)");

                entity.HasOne(d => d.JenisAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeJenisAtr)
                    .HasConstraintName("FK_atr_jenis_atr");

                entity.HasOne(d => d.KabupatenKota)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeKabupatenKota)
                    .HasConstraintName("FK_atr_kabupaten_kota");

                entity.HasOne(d => d.Kawasan)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeKawasan)
                    .HasConstraintName("FK_atr_kawasan");

                entity.HasOne(d => d.ProgressAtr)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProgressAtr)
                    .HasConstraintName("FK_atr_progress_atr");

                entity.HasOne(d => d.Provinsi)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodeProvinsi)
                    .HasConstraintName("FK_atr_provinsi");

                entity.HasOne(d => d.Pulau)
                    .WithMany(p => p.Atr)
                    .HasForeignKey(d => d.KodePulau)
                    .HasConstraintName("FK_atr_pulau");
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

            modelBuilder.Entity<PencarianRtr>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<FilterPencarianRtr>(entity =>
            {
                entity.HasNoKey();
            });
     }
    }
}