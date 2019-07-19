using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
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
            IQueryable<Models.Atr> query = _context.Atr;

            query = filter.QueryAtrByJenisList(query, this.AtrSearch);
            query = filter.QueryAtrByProvinsi(query, this.AtrSearch);
            query = filter.QueryAtrByKabupatenKota(query, this.AtrSearch);
            query = filter.QueryAtrByTahunRekomendasiGubernur(query, this.AtrSearch);
            query = filter.QueryAtrByTahunPermohonanPersetujuanSubstansi(query, this.AtrSearch);
            query = filter.QueryAtrByTahunMasukLoket(query, this.AtrSearch);
            query = filter.QueryAtrByTahunRapatLintasSektor(query, this.AtrSearch);
            query = filter.QueryAtrByTahunPersetujuanSubstansi(query, this.AtrSearch);
            query = filter.QueryAtrByTahun(query, this.AtrSearch);

            this.HasilPencarian = await query
                .Include(a => a.Provinsi)
                .Include(a => a.KabupatenKota)
                .Include(a => a.KabupatenKota.Provinsi)
                .Include(a => a.ProgressAtr)
                .Include(a => a.JenisAtr)
                .OrderBy(a => a.KabupatenKota.Provinsi.Nama)
                .ThenBy(a => a.KabupatenKota.Nama)
                .ThenBy(a => a.Provinsi.Nama)
                .ToListAsync();
            return Page();
        }

        private readonly FilterUtilities filter = new FilterUtilities();

        private readonly MonevAtrDbContext _context;
    }
}