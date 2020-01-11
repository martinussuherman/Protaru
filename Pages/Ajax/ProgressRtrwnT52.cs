using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ProgressRtrwnT52 : PageModel
    {
        public ProgressRtrwnT52(MonevAtrDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int jenisRtr)
        {
            ProgressSummary summary = new ProgressSummary
            {
                KajianPk = await CountKajianPk(jenisRtr),
                PenyusunanPk = await CountPenyusunanPk(jenisRtr),
                PenyusunanMateriTeknisRtrwnT52 = await CountPenyusunanMateriTeknisRtrwnT52(jenisRtr),
                PenyepakatanTpakRtrwnT52 = await CountPenyepakatanTpakRtrwnT52(jenisRtr),
                HarmonisasiKemenkumhamRtrwnT52 = await CountHarmonisasiKemenkumhamRtrwnT52(jenisRtr),
                PembahasanSekretariatRtrwnT52 = await CountPembahasanSekretariatRtrwnT52(jenisRtr),
                PenetapanPresidenRtrwnT52 = await CountPenetapanPresidenRtrwnT52(jenisRtr)
            };

            summary.Total =
                summary.KajianPk +
                summary.PenyusunanPk +
                summary.PenyusunanMateriTeknisRtrwnT52 +
                summary.PenyepakatanTpakRtrwnT52 +
                summary.HarmonisasiKemenkumhamRtrwnT52 +
                summary.PembahasanSekretariatRtrwnT52 +
                summary.PenetapanPresidenRtrwnT52;
            return new JsonResult(summary);
        }

        private async Task<int> CountKajianPk(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 1);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyusunanPk(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 2);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyusunanMateriTeknisRtrwnT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 3);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyepakatanTpakRtrwnT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 4);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountHarmonisasiKemenkumhamRtrwnT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 5);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPembahasanSekretariatRtrwnT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 6);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenetapanPresidenRtrwnT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 7);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private IQueryable<Models.Atr> QueryByJenisRtr(IQueryable<Models.Atr> query, int jenisRtr)
        {
            return jenisRtr == 0 ?
                query :
                query.Where(q => q.KodeJenisAtr == jenisRtr);
        }

        private class ProgressSummary
        {
            public int KajianPk { get; set; }
            public int PenyusunanPk { get; set; }
            public int PenyusunanMateriTeknisRtrwnT52 { get; set; }
            public int PenyepakatanTpakRtrwnT52 { get; set; }
            public int HarmonisasiKemenkumhamRtrwnT52 { get; set; }
            public int PembahasanSekretariatRtrwnT52 { get; set; }
            public int PenetapanPresidenRtrwnT52 { get; set; }
            public int Total { get; set; }
        }

        private readonly MonevAtrDbContext _context;
    }
}