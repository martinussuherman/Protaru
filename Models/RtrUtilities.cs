using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Protaru.Models;

namespace MonevAtr.Models
{
    public class RtrUtilities
    {
        public RtrUtilities(PomeloDbContext context)
        {
            _context = context;
        }

        public void SetCommonRtrPropertiesOnCreate(
            Atr rtr,
            JenisRtrEnum jenisRtr,
            StatusRevisi statusRevisi,
            ClaimsPrincipal user)
        {
            if (rtr.KodeProvinsi == 0)
            {
                rtr.KodeProvinsi = null;
            }

            if (rtr.KodeKabupatenKota == 0)
            {
                rtr.KodeKabupatenKota = null;
            }
            else if (rtr.KodeKabupatenKota != null)
            {
                rtr.KodeProvinsi = null;
            }

            if (rtr.KodeProgressAtr == 0)
            {
                rtr.KodeProgressAtr = null;
            }

            if (rtr.KodePulau == 0)
            {
                rtr.KodePulau = null;
            }

            if (rtr.KodeKawasan == 0)
            {
                rtr.KodeKawasan = null;
            }

            rtr.KodeJenisAtr = (int)jenisRtr;
            rtr.StatusRevisi = (sbyte)statusRevisi.Kode;
            rtr.Tahun = 0;
            rtr.PembaruanOleh = user.Identity.Name;
        }

        public async Task UpdateReferensiRtr(int kodeReferensiRtr)
        {
            Atr referensi = new Atr()
            {
                Kode = kodeReferensiRtr,
                SudahDirevisi = 1
            };

            // TODO : update forward link

            _context.Atr.Attach(referensi);
            _context.Entry(referensi)
                .Property(r => r.SudahDirevisi)
                .IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<KelompokDokumen>> LoadKelompokDokumenDanDokumen(
            int? kodeJenisRtr)
        {
            List<KelompokDokumen> result = await _context.KelompokDokumen
                .Include(k => k.Dokumen)
                .Where(k => k.KodeJenisAtr == kodeJenisRtr)
                .OrderBy(k => k.Nomor)
                .AsNoTracking()
                .ToListAsync();

            result.ForEach(k => k.Dokumen = k.Dokumen
                .OrderBy(d => d.Nomor)
                .ToList());

            return result;
        }

        public async Task MergeRtrDokumenDenganKelompokDokumen(
            Atr rtr,
            List<KelompokDokumen> kelompokDokumenList)
        {
            List<AtrDokumen> rtrDokumenList = await _context.AtrDokumen
                .Where(c => c.KodeAtr == rtr.Kode)
                .AsNoTracking()
                .ToListAsync();

            foreach (Dokumen dokumen in kelompokDokumenList.SelectMany(c => c.Dokumen))
            {
                SelaraskanIsiDokumen(rtr, dokumen, rtrDokumenList);
            }
        }

        public async Task<List<FasilitasKegiatan>> LoadFasilitasKegiatan()
        {
            return await _context.FasilitasKegiatan
                .OrderBy(f => f.Kode)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task MergeRtrFasilitasKegiatan(
            Atr rtr,
            int? id,
            List<FasilitasKegiatan> fasilitasKegiatanList)
        {
            List<RtrFasilitasKegiatan> rtrFasilitasKegiatanList =
                await _context.RtrFasilitasKegiatan
                .Where(r => r.KodeRtr == id)
                .AsNoTracking()
                .ToListAsync();

            foreach (FasilitasKegiatan kegiatan in fasilitasKegiatanList)
            {
                SelaraskanIsiFasilitasKegiatan(
                    rtr,
                    kegiatan,
                    rtrFasilitasKegiatanList);
            }
        }

        public async Task<bool> SaveRtr(Atr rtr, ClaimsPrincipal user, EntityState state)
        {
            if (rtr.KodeProgressAtr == 0)
            {
                rtr.KodeProgressAtr = null;
            }

            rtr.PembaruanOleh = user.Identity.Name;
            _context.Attach(rtr).State = state;

            // TODO : backward link

            LogUser log = new LogUser
            {
                User = user.Identity.Name,
            };

            if (state == EntityState.Added)
            {
                log.JenisKegiatan = (ushort)rtr.KodeJenisAtr;
            }
            else
            {
                log.JenisKegiatan = (ushort)(rtr.KodeJenisAtr + 1000);
            }

            await _context.LogUser.AddAsync(log);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RtrExists(rtr.Kode))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> SaveRtrDokumen(
            Atr rtr,
            AtrDokumen dokumen,
            List<Dokumen> dokumenList)
        {
            dokumen.KodeAtr = rtr.Kode;
            UpdateRtrDenganNomorDanTahunDokumen(rtr, dokumen, dokumenList);

            if (dokumen.Kode == 0)
            {
                if (!dokumen.PerluSimpan)
                {
                    return true;
                }

                _context.AtrDokumen.Add(dokumen);
                await _context.SaveChangesAsync();
                return true;
            }

            if (!dokumen.PerluSimpan)
            {
                _context.AtrDokumen.Remove(dokumen);
                await _context.SaveChangesAsync();
                return true;
            }

            _context.Attach(dokumen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RtrDokumenExists(dokumen.Kode))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> SaveRtrFasilitasKegiatan(
            Atr rtr,
            RtrFasilitasKegiatan rtrFasilitasKegiatan)
        {
            rtrFasilitasKegiatan.KodeRtr = rtr.Kode;
            rtrFasilitasKegiatan.Keterangan ??= string.Empty;

            if (rtrFasilitasKegiatan.Kode == 0)
            {
                if (!rtrFasilitasKegiatan.PerluSimpan)
                {
                    return true;
                }

                _context.RtrFasilitasKegiatan.Add(rtrFasilitasKegiatan);
                await _context.SaveChangesAsync();
                return true;
            }

            if (!rtrFasilitasKegiatan.PerluSimpan)
            {
                _context.RtrFasilitasKegiatan.Remove(rtrFasilitasKegiatan);
                await _context.SaveChangesAsync();
                return true;
            }

            _context.Attach(rtrFasilitasKegiatan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RtrFasilitasKegiatanExists(rtrFasilitasKegiatan.Kode))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private void UpdateRtrDenganNomorDanTahunDokumen(
            Atr rtr,
            AtrDokumen rtrDokumen,
            List<Dokumen> dokumenList)
        {
            if (dokumenList.Find(d => d.Kode == rtrDokumen.KodeDokumen).AmbilNomor != 1)
            {
                return;
            }

            rtr.Nomor = rtrDokumen.Nomor;

            if (rtr.Nomor == null)
            {
                rtr.Nomor = string.Empty;
            }

            if (rtrDokumen.Tanggal == DateTime.MinValue)
            {
                rtr.Tahun = 0;
                return;
            }

            rtr.Tahun = (short)rtrDokumen.Tanggal.Year;
        }

        private void SelaraskanIsiDokumen(
            Atr rtr,
            Dokumen dokumen,
            List<AtrDokumen> rtrDokumenList)
        {
            AtrDokumen joinedItem = rtrDokumenList
                .Find(k => k.KodeDokumen == dokumen.Kode);

            if (joinedItem == null)
            {
                joinedItem = new AtrDokumen
                {
                    KodeAtr = rtr.Kode,
                    KodeDokumen = dokumen.Kode
                };
            }

            joinedItem.Atr = rtr;
            joinedItem.Dokumen = dokumen;
            dokumen.DetailDokumen = joinedItem;
        }

        private void SelaraskanIsiFasilitasKegiatan(
            Atr rtr,
            FasilitasKegiatan fasilitasKegiatan,
            List<RtrFasilitasKegiatan> rtrFasilitasKegiatanList)
        {
            RtrFasilitasKegiatan joinedItem = rtrFasilitasKegiatanList
                .Find(f => f.KodeFasilitasKegiatan == fasilitasKegiatan.Kode);

            if (joinedItem == null)
            {
                joinedItem = new RtrFasilitasKegiatan
                {
                    KodeRtr = rtr.Kode,
                    KodeFasilitasKegiatan = fasilitasKegiatan.Kode
                };
            }

            joinedItem.Rtr = rtr;
            joinedItem.FasilitasKegiatan = fasilitasKegiatan;
            fasilitasKegiatan.DetailFasilitasKegiatan = joinedItem;
        }

        private bool RtrExists(int kode)
        {
            return _context.Atr.Any(e => e.Kode == kode);
        }

        private bool RtrDokumenExists(int kode)
        {
            return _context.AtrDokumen.Any(e => e.Kode == kode);
        }

        private bool RtrFasilitasKegiatanExists(int kode)
        {
            return _context.RtrFasilitasKegiatan.Any(e => e.Kode == kode);
        }

        private readonly PomeloDbContext _context;
    }
}