using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class FilterUtilities
    {
        // TODO : deprecated when query via PencarianRtr is done
        public IQueryable<Atr> QueryAtrByProvinsi(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.Prov == 0 || search.KabKota != 0 ?
                query :
                query.Where(q => q.KodeProvinsi == search.Prov ||
                    q.KabupatenKota.KodeProvinsi == search.Prov);
        }

        public IQueryable<Atr> QueryAtrByKabupatenKota(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.KabKota == 0 ?
                query :
                query.Where(q => q.KodeKabupatenKota == search.KabKota);
        }

        public IQueryable<Atr> QueryAtrByPulau(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.Prov == 0 || search.KabKota != 0 ?
                query :
                query.Where(q => q.KodeProvinsi == search.Prov ||
                    q.KabupatenKota.KodeProvinsi == search.Prov);
        }

        public IQueryable<Atr> QueryAtrByTahun(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.Tahun == 0 ?
                query :
                query.Where(q => q.Tahun == search.Tahun);
        }

        public IQueryable<Atr> QueryAtrByNama(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            if (String.IsNullOrEmpty(search.Nama))
            {
                return query;
            }

            string pattern = search.Nama + "%";
            return query.Where(q => EF.Functions.Like(q.Nama, pattern));
        }

        public IQueryable<Atr> QueryAtrByNomor(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            if (String.IsNullOrEmpty(search.Nomor))
            {
                return query;
            }

            string pattern = search.Nomor + "%";
            return query.Where(q => EF.Functions.Like(q.Nomor, pattern));
        }

        public IQueryable<Atr> QueryAtrByProgress(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            ExpressionStarter<Atr> predicate = PredicateBuilder.New<Atr>(true);

            foreach (int kodeProgress in search.ProgressList)
            {
                predicate = predicate.Or(p => p.KodeProgressAtr == kodeProgress);
            }

            return query.Where(predicate);
        }
    }

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
            return query.Where(q => q.ProgressAtr.IsPerdaPerpres == search.Perda);
        }

        public static IQueryable<Atr> ByNama(
            this IQueryable<Atr> query,
            string nama)
        {
            if (String.IsNullOrEmpty(nama))
            {
                return query;
            }

            string pattern = nama + "%";
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
            return tahun == 0 ?
                query :
                query.Where(q => q.Tahun == tahun);
        }

        public static IQueryable<Atr> ByJenis(
            this IQueryable<Atr> query,
            JenisRtrEnum jenis)
        {
            return query.Where(a => a.KodeJenisAtr == (int)jenis);
        }

        public static IQueryable<Atr> ByJenisList(
            this IQueryable<Atr> query,
            AtrSearch search)
        {
            ExpressionStarter<Atr> predicate =
                PredicateBuilder.New<Atr>(true);

            foreach (int kode in search.JenisList)
            {
                predicate = predicate.Or(p => p.KodeJenisAtr == kode);
            }

            return query.Where(predicate);
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