using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class ListLintasSektor : PageModel
    {
        public ListLintasSektor(MonevAtrDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetAsync(int tahun, int bulan, int isRdtr)
        {
            IQueryable<PencarianRtr> query = _context.PencarianRtr;
            query = QueryJenisRtr(query, isRdtr);
            query = QueryKodeDokumen(query, isRdtr);

            List<PencarianRtr> result = await query
                .Where(q => q.TahunDokumen == tahun && q.BulanDokumen == bulan)
                .Take(20)
                .AsNoTracking()
                .ToListAsync();

            return new JsonResult(result);
        }

        private IQueryable<PencarianRtr> QueryKodeDokumen(IQueryable<PencarianRtr> query, int isRdtr)
        {
            if (isRdtr == 1)
            {
                return query.Where(q =>
                    q.KodeDokumen == 30 ||
                    q.KodeDokumen == 177
                );
            }

            return query.Where(q =>
                q.KodeDokumen == 64 ||
                q.KodeDokumen == 100 ||
                q.KodeDokumen == 138
            );
        }

        private IQueryable<PencarianRtr> QueryJenisRtr(IQueryable<PencarianRtr> query, int isRdtr)
        {
            if (isRdtr == 1)
            {
                return query.Where(q =>
                    q.KodeJenisRtr == 1 ||
                    q.KodeJenisRtr == 2);
            }

            return query.Where(q =>
                q.KodeJenisRtr == 3 ||
                q.KodeJenisRtr == 4 ||
                q.KodeJenisRtr == 5);
        }

        private readonly MonevAtrDbContext _context;
    }
}