using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ProgressT52 : PageModel
    {
        public ProgressT52(PomeloDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int jenisRtr)
        {
            ProgressSummary summary = new ProgressSummary
            {
                ProsesPK = await CountProsesPK(jenisRtr),
                Revisi = await CountRevisi(jenisRtr),
                RekomendasiGubernur = await CountRekomendasiGubernur(jenisRtr),
                PersetujuanSubstansi = await CountPersetujuanSubstansi(jenisRtr),
                Perda = await CountPerda(jenisRtr)
            };

            summary.Total =
                summary.ProsesPK +
                summary.Revisi +
                summary.RekomendasiGubernur +
                summary.PersetujuanSubstansi +
                summary.Perda;
            return new JsonResult(summary);
        }

        private async Task<int> CountProsesPK(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor >= 1 && q.ProgressAtr.Nomor <= 5);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountRevisi(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 6 || q.ProgressAtr.Nomor == 7);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountRekomendasiGubernur(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 8);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPersetujuanSubstansi(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 9 || q.ProgressAtr.Nomor == 10);

            query = QueryByJenisRtr(query, jenisRtr);
            return await query.AsNoTracking().CountAsync();
        }

        private async Task<int> CountPerda(int jenisRtr)
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(q => q.ProgressAtr.Nomor == 11);

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
            public int ProsesPK { get; set; }

            public int Revisi { get; set; }

            public int RekomendasiGubernur { get; set; }

            public int PersetujuanSubstansi { get; set; }

            public int Perda { get; set; }

            public int Total { get; set; }
        }

        private readonly PomeloDbContext _context;
    }
}