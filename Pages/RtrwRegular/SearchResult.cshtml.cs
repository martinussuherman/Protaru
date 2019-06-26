using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRegular
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AtrSearch AtrSearch { get; set; }

        public List<Models.Atr> HasilPencarian { get; set; } = new List<Models.Atr>();

        public async Task<IActionResult> OnPost()
        {
            IQueryable<Models.Atr> query = (from atr in _context.Atr where atr.KodeJenisAtr == (int) JenisAtrEnum.RtrwRegular select atr);

            query = QueryAtrByProvinsi(query);
            query = QueryAtrByKabupatenKota(query);
            query = QueryAtrByTahun(query);
            query = QueryAtrByNama(query);
            query = QueryAtrByNomor(query);
            query = QueryAtrByProgress(query);

            this.HasilPencarian = await query
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.JenisAtr)
                .ToListAsync();
            return Page();
        }

        private IQueryable<Models.Atr> QueryAtrByProvinsi(IQueryable<Models.Atr> query)
        {
            if (this.AtrSearch.KodeProvinsi == 0 || this.AtrSearch.KodeKabupatenKota != 0)
            {
                return query;
            }

            return query.Where(q => q.KodeProvinsi == this.AtrSearch.KodeProvinsi);
        }

        private IQueryable<Models.Atr> QueryAtrByKabupatenKota(IQueryable<Models.Atr> query)
        {
            if (this.AtrSearch.KodeKabupatenKota == 0)
            {
                return query;
            }

            return query.Where(q => q.KodeKabupatenKota == this.AtrSearch.KodeKabupatenKota);
        }

        private IQueryable<Models.Atr> QueryAtrByTahun(IQueryable<Models.Atr> query)
        {
            if (this.AtrSearch.Tahun == 0)
            {
                return query;
            }

            return query.Where(q => q.Tahun == this.AtrSearch.Tahun);
        }

        private IQueryable<Models.Atr> QueryAtrByNama(IQueryable<Models.Atr> query)
        {
            if (String.IsNullOrEmpty(this.AtrSearch.Nama))
            {
                return query;
            }

            string pattern = this.AtrSearch.Nama + "%";
            return query.Where(q => EF.Functions.Like(q.Nama, pattern));
        }

        private IQueryable<Models.Atr> QueryAtrByNomor(IQueryable<Models.Atr> query)
        {
            if (String.IsNullOrEmpty(this.AtrSearch.Nomor))
            {
                return query;
            }

            string pattern = this.AtrSearch.Nomor + "%";
            return query.Where(q => EF.Functions.Like(q.Nomor, pattern));
        }

        private IQueryable<Models.Atr> QueryAtrByProgress(IQueryable<Models.Atr> query)
        {
            var predicate = PredicateBuilder.New<Models.Atr>();

            foreach (int kodeProgress in this.AtrSearch.KodeProgressAtr)
            {
                predicate = predicate.Or(p => p.KodeProgressAtr == kodeProgress);
            }

            return query.Where(predicate);
        }

        private readonly MonevAtrDbContext _context;
    }
}