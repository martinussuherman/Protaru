using System;
using System.Linq;
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
    }
}