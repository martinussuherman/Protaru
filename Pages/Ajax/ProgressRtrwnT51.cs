using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ProgressRtrwnT51 : PageModel
    {
        public ProgressRtrwnT51(MonevAtrDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int jenisRtr)
        {
            ProgressSummary summary = new ProgressSummary
            {
                PenyusunanMateriTeknis = await CountPenyusunanMateriTeknis(jenisRtr),
                PenyepakatanTpak = await CountPenyepakatanTpak(jenisRtr),
                HarmonisasiKemenkumham = await CountHarmonisasiKemenkumham(jenisRtr),
                PembahasanSekretariat = await CountPembahasanSekretariat(jenisRtr),
                PenetapanPresiden = await CountPenetapanPresiden(jenisRtr)
            };

            summary.Total =
                summary.PenyusunanMateriTeknis +
                summary.PenyepakatanTpak +
                summary.HarmonisasiKemenkumham +
                summary.PembahasanSekretariat +
                summary.PenetapanPresiden;
            return new JsonResult(summary);
        }

        private async Task<int> CountPenyusunanMateriTeknis(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 1);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenyepakatanTpak(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 2);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountHarmonisasiKemenkumham(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 3);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPembahasanSekretariat(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 4);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPenetapanPresiden(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 5);

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
            public int PenyusunanMateriTeknis { get; set; }
            public int PenyepakatanTpak { get; set; }
            public int HarmonisasiKemenkumham { get; set; }
            public int PembahasanSekretariat { get; set; }
            public int PenetapanPresiden { get; set; }
            public int Total { get; set; }
        }

        private readonly MonevAtrDbContext _context;
    }
}