using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Protaru.Models;

namespace MonevAtr.Models
{
    public static class FilterUtilitiesExtensions
    {
        public static List<int> KodeDokumenRekomendasiGubernur
        {
            get
            {
                if (_kodeDokumenRekomendasiGubernur == null)
                {
                    _kodeDokumenRekomendasiGubernur = new List<int>
                    {
                    24,
                    58,
                    97,
                    132,
                    171
                    };
                }

                return _kodeDokumenRekomendasiGubernur;
            }
        }

        public static List<int> KodeDokumenPermohonanPersetujuanSubstansi
        {
            get
            {
                if (_kodeDokumenPermohonanPersetujuanSubstansi == null)
                {
                    _kodeDokumenPermohonanPersetujuanSubstansi = new List<int>
                    {
                    25,
                    59,
                    98,
                    133,
                    172
                    };
                }

                return _kodeDokumenPermohonanPersetujuanSubstansi;
            }
        }

        public static List<int> KodeDokumenMasukLoket
        {
            get
            {
                if (_kodeDokumenMasukLoket == null)
                {
                    _kodeDokumenMasukLoket = new List<int>
                    {
                    29,
                    63,
                    99,
                    137,
                    176
                    };
                }

                return _kodeDokumenMasukLoket;
            }
        }

        public static List<int> KodeDokumenRapatLintasSektor
        {
            get
            {
                if (_kodeDokumenRapatLintasSektor == null)
                {
                    _kodeDokumenRapatLintasSektor = new List<int>
                    {
                    30,
                    64,
                    100,
                    138,
                    177
                    };
                }

                return _kodeDokumenRapatLintasSektor;
            }
        }

        public static List<int> KodeDokumenPersetujuanSubstansi
        {
            get
            {
                if (_kodeDokumenPersetujuanSubstansi == null)
                {
                    _kodeDokumenPersetujuanSubstansi = new List<int>
                    {
                    31,
                    65,
                    101,
                    139,
                    178
                    };
                }

                return _kodeDokumenPersetujuanSubstansi;
            }
        }

        public static IQueryable<FilterPencarianRtr> ByProvinsi(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return search.Prov == 0 || search.KabKota != 0 ?
                query :
                query.Where(q => q.KodeProvinsi == search.Prov ||
                    q.KodeProvinsiKabupatenKota == search.Prov);
        }
        public static IQueryable<Atr> ByProvinsi(
            this IQueryable<Atr> query,
            int kodeProvinsi,
            int kodeKabupatenKota)
        {
            return kodeProvinsi == 0 || kodeKabupatenKota != 0 ?
                query :
                query.Where(q => q.KodeProvinsi == kodeProvinsi ||
                    q.KabupatenKota.KodeProvinsi == kodeProvinsi);
        }

        public static IQueryable<FilterPencarianRtr> ByKabupatenKota(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return search.KabKota == 0 ?
                query :
                query.Where(q => q.KodeKabupatenKota == search.KabKota);
        }
        public static IQueryable<Atr> DaerahByProgressNoTracking(
            this IQueryable<Atr> query,
            AtrSearch rtr,
            JenisRtrEnum jenis)
        {
            return query
                .ByJenis(jenis)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByIsPerdaPerpres(rtr)
                .Where(c => c.SudahDirevisi == 0)
                .RtrInclude()
                .AsNoTracking();
        }
        public static IQueryable<Atr> ByKabupatenKota(
            this IQueryable<Atr> query,
            int kodeKabupatenKota)
        {
            return kodeKabupatenKota == 0 ?
                query :
                query.Where(q => q.KodeKabupatenKota == kodeKabupatenKota);
        }

        public static IQueryable<Atr> ByIsPerdaPerpres(
            this IQueryable<Atr> query, AtrSearch search)
        {
            return search.Perda == 1000
                ? query
                : query.Where(q => q.ProgressAtr.IsPerdaPerpres == search.Perda);
        }

        public static IQueryable<Atr> ByNama(
            this IQueryable<Atr> query,
            string nama)
        {
            if (String.IsNullOrEmpty(nama))
            {
                return query;
            }

            string pattern = "%" + nama + "%";
            return query.Where(q => EF.Functions.Like(q.Nama, pattern));
        }

        public static IQueryable<Atr> ByNomor(
            this IQueryable<Atr> query,
            string nomor)
        {
            if (String.IsNullOrEmpty(nomor))
            {
                return query;
            }

            string pattern = nomor + "%";
            return query.Where(q => EF.Functions.Like(q.Nomor, pattern));
        }

        public static IQueryable<Atr> ByTahun(
            this IQueryable<Atr> query,
            int tahun)
        {
            return tahun < 0 ?
                query :
                query.Where(q => q.Tahun == tahun);
        }

        public static IQueryable<Atr> ByJenis(
            this IQueryable<Atr> query,
            JenisRtrEnum jenis)
        {
            if (jenis == JenisRtrEnum.All)
            {
                return query;
            }

            if (jenis == JenisRtrEnum.Daerah)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RdtrT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RdtrT52 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT50 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT52);
            }

            if (jenis == JenisRtrEnum.Rdtr)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RdtrT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RdtrT52);
            }

            if (jenis == JenisRtrEnum.Rtrw)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT50 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwT52);
            }

            if (jenis == JenisRtrEnum.Nasional)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT52 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT52 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT52 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwnT52);
            }

            if (jenis == JenisRtrEnum.RtrKpn)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKpnT52);
            }

            if (jenis == JenisRtrEnum.RtrKsn)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrKsnT52);
            }

            if (jenis == JenisRtrEnum.RtrPulau)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrPulauT52);
            }

            if (jenis == JenisRtrEnum.Rtrwn)
            {
                return query.Where(q =>
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwnT51 ||
                    q.KodeJenisAtr == (int)JenisRtrEnum.RtrwnT52);
            }

            return query.Where(q => q.KodeJenisAtr == (int)jenis);
        }

        public static IQueryable<Atr> ByPulau(
            this IQueryable<Atr> query,
            int kodePulau)
        {
            return kodePulau == 0 ?
                query :
                query.Where(q => q.KodePulau == kodePulau);
        }

        public static IQueryable<Atr> ByKawasan(
            this IQueryable<Atr> query,
            int kodeKawasan)
        {
            return kodeKawasan == 0 ?
                query :
                query.Where(q => q.KodeKawasan == kodeKawasan);
        }

        public static IQueryable<Atr> ByT52CreateFilter(
            this IQueryable<Atr> query,
            JenisRtrEnum jenisT51,
            JenisRtrEnum jenisT52)
        {
            return query
                .Where(a =>
                    ((a.KodeJenisAtr == (int)jenisT51 && a.StatusRevisi >= StatusRevisi.RegularT52.Kode)
                        || a.KodeJenisAtr == (int)jenisT52)
                    && a.SudahDirevisi == 0);
        }

        public static IQueryable<Atr> RtrInclude(this IQueryable<Atr> query)
        {
            return query
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .Include(a => a.JenisAtr)
                .OrderBy(a => a.Provinsi.Nama)
                .ThenBy(a => a.KabupatenKota.Provinsi.Nama)
                .ThenBy(a => a.KabupatenKota.Nama);
        }

        public static IQueryable<Atr> RtrIncludeAll(this IQueryable<Atr> query)
        {
            return query
                .Include(q => q.Provinsi)
                .Include(q => q.KabupatenKota)
                .Include(q => q.KabupatenKota.Provinsi)
                .Include(q => q.Pulau)
                .Include(q => q.Kawasan)
                .Include(q => q.ProgressAtr)
                .Include(q => q.JenisAtr)
                .OrderBy(q => q.Provinsi.Nama)
                .ThenBy(q => q.KabupatenKota.Provinsi.Nama)
                .ThenBy(q => q.KabupatenKota.Nama)
                .ThenBy(q => q.Pulau.Nama)
                .ThenBy(q => q.Kawasan.Nama);
        }

        public static IQueryable<Atr> RtrPulauInclude(this IQueryable<Atr> query)
        {
            return query
                .Include(a => a.Pulau)
                .Include(a => a.ProgressAtr)
                .Include(a => a.JenisAtr)
                .OrderBy(a => a.Pulau.Nama);
        }

        public static IQueryable<Atr> RtrKsnInclude(this IQueryable<Atr> query)
        {
            return query
                .Include(a => a.Kawasan)
                .Include(a => a.ProgressAtr)
                .Include(a => a.JenisAtr)
                .OrderBy(a => a.Kawasan.Nama);
        }

        public static IQueryable<T> ByKodeList<T>(
            this IQueryable<T> query,
            IEnumerable<int> list) where T : IKode
        {
            ExpressionStarter<T> predicate =
                PredicateBuilder.New<T>(false);

            foreach (int kode in list)
            {
                predicate = predicate.Or(p => p.Kode == kode);
            }

            return query.Where(predicate);
        }

        public static IQueryable<FilterPencarianRtr> ByJenisList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            ExpressionStarter<FilterPencarianRtr> predicate =
                PredicateBuilder.New<FilterPencarianRtr>(true);

            foreach (int kode in search.JenisList)
            {
                predicate = predicate.Or(p => p.KodeJenisRtr == kode);
            }

            return query.Where(predicate);
        }

        public static IQueryable<Atr> ByProgressList(
            this IQueryable<Atr> query,
            List<int> progressList)
        {
            ExpressionStarter<Atr> predicate =
                PredicateBuilder.New<Atr>(true);

            foreach (int kodeProgress in progressList)
            {
                predicate = predicate.Or(p => p.KodeProgressAtr == kodeProgress);
            }

            return query.Where(predicate);
        }

        public static IQueryable<FilterPencarianRtr> ByFasilitasKegiatanList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            ExpressionStarter<FilterPencarianRtr> predicate =
                PredicateBuilder.New<FilterPencarianRtr>(true);

            foreach (int kode in search.Faskeg)
            {
                predicate = predicate.Or(p => p.KodeFasilitasKegiatan == kode);
            }

            return query.Where(predicate);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunRekomendasiGubernurList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return query.ByTahunDokumen(
                KodeDokumenRekomendasiGubernur,
                search.RekGubList);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunPermohonanPersetujuanSubstansiList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return query.ByTahunDokumen(
                KodeDokumenPermohonanPersetujuanSubstansi,
                search.PerPerSubList);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunMasukLoketList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return query.ByTahunDokumen(
                KodeDokumenMasukLoket,
                search.MasLokList);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunRapatLintasSektorList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return query.ByTahunDokumen(
                KodeDokumenRapatLintasSektor,
                search.LinSekList);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunPersetujuanSubstansiList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return query.ByTahunDokumen(
                KodeDokumenPersetujuanSubstansi,
                search.PerSubList);
        }

        public static IQueryable<FilterPencarianRtr> ByTahunPerdaList(
            this IQueryable<FilterPencarianRtr> query,
            AtrSearch search)
        {
            return search.PerdaList.Count == 0 ?
                query :
                query.Where(QueryByTahunPerda(search.PerdaList));
        }

        public static Expression<Func<FilterPencarianRtr, bool>> QueryByKodeDokumen(
            List<int> kodeDokumenList)
        {
            ExpressionStarter<FilterPencarianRtr> predicate =
                PredicateBuilder.New<FilterPencarianRtr>(true);

            foreach (int kode in kodeDokumenList)
            {
                predicate = predicate.Or(p => p.KodeDokumen == kode);
            }

            return predicate;
        }

        private static IQueryable<FilterPencarianRtr> ByTahunDokumen(
            this IQueryable<FilterPencarianRtr> query,
            List<int> kodeDokumenList,
            List<int> tahunDokumenList)
        {
            return tahunDokumenList.Count == 0 ?
                query :
                query
                .Where(QueryByKodeDokumen(kodeDokumenList))
                .Where(QueryByTahunDokumen(tahunDokumenList));
        }
        private static Expression<Func<FilterPencarianRtr, bool>> QueryByTahunDokumen(
            List<int> tahunList)
        {
            ExpressionStarter<FilterPencarianRtr> predicate =
                PredicateBuilder.New<FilterPencarianRtr>(true);

            foreach (int tahun in tahunList)
            {
                predicate = predicate.Or(p => p.TahunDokumen == tahun);
            }

            return predicate;
        }

        private static Expression<Func<FilterPencarianRtr, bool>> QueryByTahunPerda(
            List<int> tahunList)
        {
            ExpressionStarter<FilterPencarianRtr> predicate =
                PredicateBuilder.New<FilterPencarianRtr>(true);

            foreach (int tahun in tahunList)
            {
                predicate = predicate.Or(p => p.Tahun == tahun);
            }

            return predicate;
        }

        private static List<int> _kodeDokumenRekomendasiGubernur;

        private static List<int> _kodeDokumenPermohonanPersetujuanSubstansi;

        private static List<int> _kodeDokumenMasukLoket;

        private static List<int> _kodeDokumenRapatLintasSektor;

        private static List<int> _kodeDokumenPersetujuanSubstansi;
    }
}