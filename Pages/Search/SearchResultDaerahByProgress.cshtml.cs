using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Search
{
    public class SearchResultDaerahByProgress : SearchResultPageModel
    {
        public SearchResultDaerahByProgress(PomeloDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            FilterByJenis(rtr);

            var result = await _context.Atr
                .ByJenisList(rtr)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .ByIsPerdaPerpres(rtr)
                .Where(c => c.SudahDirevisi == 0)
                .RtrInclude()
                .AsNoTracking()
                .ToListAsync();

            // todo merge with RtrwT5152Kabkota, RtrwT5152Provinsi, RdtrT5152Kabkota, RdtrT5152Provinsi
            if (rtr.Perda == 1)
            {
                List<int> combined = new List<int>();

                combined.AddRange(await _context.RtrwT5152Kabkota
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
                combined.AddRange(await _context.RtrwT5152Provinsi
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
                combined.AddRange(await _context.RdtrT5152Kabkota
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
                combined.AddRange(await _context.RdtrT5152Provinsi
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());

                var addedResult = await _context.Atr
                    .ByJenisList(rtr)
                    .ByProvinsi(rtr.Prov, rtr.KabKota)
                    .ByKabupatenKota(rtr.KabKota)
                    .ByTahun(rtr.Tahun)
                    .ByNama(rtr.Nama)
                    .ByNomor(rtr.Nomor)
                    .Where(c => combined.Contains(c.Kode))
                    .RtrInclude()
                    .AsNoTracking()
                    .ToListAsync();

                result.AddRange(addedResult);
            }


            Hasil = result.ToPagerList(page, PagerUrlHelper.ItemPerPage);
            Rtr = rtr;
            RegulationName = "Perda";
            IsDisplayRegulation = (rtr.Perda == 1);
            IsUseCreateForm = false;
            IsCanCreate = false;
            IsCanEdit = false;

            return Page();
        }

        private void FilterByJenis(AtrSearch rtr)
        {
            rtr.JenisList.Add((int)JenisRtrEnum.RdtrT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RdtrT52);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT50);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT51);
            rtr.JenisList.Add((int)JenisRtrEnum.RtrwT52);
        }

        private readonly PomeloDbContext _context;
    }
}