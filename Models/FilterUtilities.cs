using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public class FilterUtilities
    {
        public IQueryable<Atr> QueryAtrByProvinsi(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.KodeProvinsi == 0 || search.KodeKabupatenKota != 0 ?
                query :
                query.Where(q => q.KodeProvinsi == search.KodeProvinsi ||
                    q.KabupatenKota.KodeProvinsi == search.KodeProvinsi);
        }

        public IQueryable<Atr> QueryAtrByKabupatenKota(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.KodeKabupatenKota == 0 ?
                query :
                query.Where(q => q.KodeKabupatenKota == search.KodeKabupatenKota);
        }

        public IQueryable<Atr> QueryAtrByTahun(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.Tahun == 0 ?
                query :
                query.Where(q => q.Tahun == search.Tahun);
        }

        public IQueryable<Atr> QueryAtrByTahunRekomendasiGubernur(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.TahunRekomendasiGubernur == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(
                    a => (a.KodeDokumen == 24 || a.KodeDokumen == 58 || a.KodeDokumen == 97) &&
                    a.Tanggal.Year == search.TahunRekomendasiGubernur));
        }

        public IQueryable<Atr> QueryAtrByTahunPermohonanPersetujuanSubstansi(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.TahunPermohonanPersetujuanSubstansi == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(
                    a => (a.KodeDokumen == 25 || a.KodeDokumen == 59 || a.KodeDokumen == 98) &&
                    a.Tanggal.Year == search.TahunPermohonanPersetujuanSubstansi));

        }

        public IQueryable<Atr> QueryAtrByTahunMasukLoket(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.TahunMasukLoket == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(
                    a => (a.KodeDokumen == 29 || a.KodeDokumen == 63 || a.KodeDokumen == 99) &&
                    a.Tanggal.Year == search.TahunMasukLoket));
        }

        public IQueryable<Atr> QueryAtrByTahunRapatLintasSektor(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.TahunRapatLintasSektor == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(
                    a => (a.KodeDokumen == 30 || a.KodeDokumen == 64 || a.KodeDokumen == 100) &&
                    a.Tanggal.Year == search.TahunRapatLintasSektor));
        }

        public IQueryable<Atr> QueryAtrByTahunPersetujuanSubstansi(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.TahunPersetujuanSubstansi == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(
                    a => (a.KodeDokumen == 31 || a.KodeDokumen == 65 || a.KodeDokumen == 101) &&
                    a.Tanggal.Year == search.TahunPersetujuanSubstansi));
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

            foreach (int kodeProgress in search.KodeProgressAtr)
            {
                predicate = predicate.Or(p => p.KodeProgressAtr == kodeProgress);
            }

            return query.Where(predicate);
        }

        public IQueryable<Atr> QueryAtrByDokumen(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            return search.KodeDokumen == 0 ?
                query :
                query.Where(q => q.AtrDokumen.Any(a => a.KodeDokumen == search.KodeDokumen));
        }

        public IQueryable<Atr> QueryAtrByJenisList(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            ExpressionStarter<Atr> predicate = PredicateBuilder.New<Atr>(true);

            foreach (int kode in search.KodeJenisAtrList)
            {
                predicate = predicate.Or(p => p.KodeJenisAtr == kode);
            }

            return query.Where(predicate);
        }

        public IQueryable<Atr> QueryAtrByDokumenList(
            IQueryable<Atr> query,
            AtrSearch search)
        {
            ExpressionStarter<AtrDokumen> predicate = PredicateBuilder.New<AtrDokumen>(true);

            foreach (int kodeDokumen in search.KodeDokumenList)
            {
                predicate = predicate.Or(p => p.KodeDokumen == kodeDokumen);
            }

            Expression<Func<AtrDokumen, bool>> castPredicate = predicate;

            return query.Where(p => p.AtrDokumen.Any(a => castPredicate.Invoke(a)));
        }
    }
}