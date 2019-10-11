using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ProgressT51 : PageModel
    {
        public ProgressT51(MonevAtrDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int jenisRtr)
        {
            ProgressSummary summary = new ProgressSummary
            {
                Penyusunan = await CountPenyusunan(jenisRtr),
                RekomendasiGubernur = await CountRekomendasiGubernur(jenisRtr),
                PersetujuanSubstansi = await CountPersetujuanSubstansi(jenisRtr),
                Perda = await CountPerda(jenisRtr)
            };

            summary.Total =
                summary.Penyusunan +
                summary.RekomendasiGubernur +
                summary.PersetujuanSubstansi +
                summary.Perda;
            return new JsonResult(summary);
        }

        private async Task<int> CountPenyusunan(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 1 || q.ProgressAtr.Nomor == 2);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.CountAsync();
        }

        private async Task<int> CountRekomendasiGubernur(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 3);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.CountAsync();
        }

        private async Task<int> CountPersetujuanSubstansi(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 4 || q.ProgressAtr.Nomor == 5);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.CountAsync();
        }

        private async Task<int> CountPerda(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Include(q => q.ProgressAtr)
                .Where(q => q.ProgressAtr.Nomor == 6);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.CountAsync();
        }

        private IQueryable<Models.Atr> QueryByJenisRtr(IQueryable<Models.Atr> query, int jenisRtr)
        {
            return jenisRtr == 0 ?
                query :
                query.Where(q => q.KodeJenisAtr == jenisRtr);
        }

        private class ProgressSummary
        {
            public int Penyusunan { get; set; }

            public int RekomendasiGubernur { get; set; }

            public int PersetujuanSubstansi { get; set; }

            public int Perda { get; set; }

            public int Total { get; set; }
        }

        private readonly MonevAtrDbContext _context;
    }
}