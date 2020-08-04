using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ProgressPulauKsnKpnT52 : PageModel
    {
        public ProgressPulauKsnKpnT52(PomeloDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int jenisRtr)
        {
            ProgressSummary summary = new ProgressSummary
            {
                KajianPk = await CountKajianPk(jenisRtr),
                PenyusunanPk = await CountPenyusunanPk(jenisRtr),
                PenyusunanMateriTeknisPulauT52 = await CountPenyusunanMateriTeknisPulauT52(jenisRtr),
                PenyepakatanDaerahPulauT52 = await CountPenyepakatanDaerahPulauT52(jenisRtr),
                PenyepakatanTpakPulauT52 = await CountPenyepakatanTpakPulauT52(jenisRtr),
                HarmonisasiKemenkumhamPulauT52 = await CountHarmonisasiKemenkumhamPulauT52(jenisRtr),
                PembahasanSekretariatPulauT52 = await CountPembahasanSekretariatPulauT52(jenisRtr),
                PenetapanPresidenPulauT52 = await CountPenetapanPresidenPulauT52(jenisRtr)
            };

            summary.Total =
                summary.KajianPk +
                summary.PenyusunanPk +
                summary.PenyusunanMateriTeknisPulauT52 +
                summary.PenyepakatanDaerahPulauT52 +
                summary.PenyepakatanTpakPulauT52 +
                summary.HarmonisasiKemenkumhamPulauT52 +
                summary.PembahasanSekretariatPulauT52 +
                summary.PenetapanPresidenPulauT52;
            return new JsonResult(summary);
        }

        private async Task<int> CountKajianPk(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 1);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyusunanPk(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 2);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyusunanMateriTeknisPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 3);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyepakatanDaerahPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 4);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyepakatanTpakPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 5);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountHarmonisasiKemenkumhamPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 6);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPembahasanSekretariatPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 7);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenetapanPresidenPulauT52(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 8);

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
            public int PenyusunanMateriTeknisPulauT52 { get; set; }
            public int PenyepakatanDaerahPulauT52 { get; set; }
            public int PenyepakatanTpakPulauT52 { get; set; }
            public int HarmonisasiKemenkumhamPulauT52 { get; set; }
            public int PembahasanSekretariatPulauT52 { get; set; }
            public int PenetapanPresidenPulauT52 { get; set; }
            public int Total { get; set; }
        }

        private readonly PomeloDbContext _context;
    }
}