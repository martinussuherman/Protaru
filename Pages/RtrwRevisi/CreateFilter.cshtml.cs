using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRevisi
{
    [Authorize]
    public class CreateFilterModel : PageModel
    {
        public CreateFilterModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AtrSearch AtrSearch { get; set; }

        public List<Models.Atr> HasilPencarian { get; set; } = new List<Models.Atr>();

        public async Task<IActionResult> OnPost()
        {
            IQueryable<Models.Atr> query = _context.Atr
                .Where(a => ((a.KodeJenisAtr == (int) JenisAtrEnum.RtrwRegular &&
                            a.StatusRevisi >= 2) ||
                        a.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi) &&
                    a.SudahDirevisi == 0);

            query = filter.QueryAtrByProvinsi(query, this.AtrSearch);
            query = filter.QueryAtrByKabupatenKota(query, this.AtrSearch);
            query = filter.QueryAtrByTahun(query, this.AtrSearch);
            query = filter.QueryAtrByNama(query, this.AtrSearch);
            query = filter.QueryAtrByNomor(query, this.AtrSearch);
            query = filter.QueryAtrByProgress(query, this.AtrSearch);

            this.HasilPencarian = await query
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.JenisAtr)
                .ToListAsync();
            return Page();
        }

        private readonly FilterUtilities filter = new FilterUtilities();

        private readonly MonevAtrDbContext _context;
    }
}