using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
{
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context, PomeloDbContext pomeloDbContext)
        {
            _pomeloDbContext = pomeloDbContext;
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IList<Models.JenisAtr> JenisAtr => _context.JenisAtr
            .OrderBy(e => e.Nomor)
            .Where(e => e.Perencanaan == 0)
            .AsNoTracking()
            .ToList();

        public IList<FasilitasKegiatan> FasilitasKegiatan => _context.FasilitasKegiatan
            .OrderBy(f => f.Kode)
            .AsNoTracking()
            .ToList();

        public IList<int> TahunRekomendasiGubernur => _pomeloDbContext.AtrDokumen
            .ByKodeList(FilterUtilitiesExtensions.KodeDokumenRekomendasiGubernur)
            .OrderBy(a => a.Tanggal.Year)
            .AsNoTracking()
            .Select(a => a.Tanggal.Year)
            .Distinct()
            .Where(y => y > 1)
            .ToList();

        public IList<int> TahunPermohonanPersetujuanSubstansi => _pomeloDbContext.AtrDokumen
            .ByKodeList(FilterUtilitiesExtensions.KodeDokumenPermohonanPersetujuanSubstansi)
            .OrderBy(a => a.Tanggal.Year)
            .AsNoTracking()
            .Select(a => a.Tanggal.Year)
            .Distinct()
            .Where(y => y > 1)
            .ToList();

        public IList<int> TahunMasukLoket => _pomeloDbContext.AtrDokumen
            .ByKodeList(FilterUtilitiesExtensions.KodeDokumenMasukLoket)
            .OrderBy(a => a.Tanggal.Year)
            .AsNoTracking()
            .Select(a => a.Tanggal.Year)
            .Distinct()
            .Where(y => y > 1)
            .ToList();

        public IList<int> TahunRapatLintasSektor => _pomeloDbContext.AtrDokumen
            .ByKodeList(FilterUtilitiesExtensions.KodeDokumenRapatLintasSektor)
            .OrderBy(a => a.Tanggal.Year)
            .AsNoTracking()
            .Select(a => a.Tanggal.Year)
            .Distinct()
            .Where(y => y > 1)
            .ToList();

        public IList<int> TahunPersetujuanSubstansi => _pomeloDbContext.AtrDokumen
            .ByKodeList(FilterUtilitiesExtensions.KodeDokumenPersetujuanSubstansi)
            .OrderBy(a => a.Tanggal.Year)
            .AsNoTracking()
            .Select(a => a.Tanggal.Year)
            .Distinct()
            .Where(y => y > 1)
            .ToList();

        public IList<int> TahunPerda
        {
            get
            {
                List<short> list = _pomeloDbContext.Atr
                    .OrderBy(a => a.Tahun)
                    .AsNoTracking()
                    .Select(a => a.Tahun)
                    .Distinct()
                    .Where(y => y > 1)
                    .ToList();

                List<int> result = new List<int>();

                foreach (short value in list)
                {
                    result.Add(value);
                }

                return result;
            }
        }

        // public IList<Models.Dokumen> Dokumen
        // {
        //     get
        //     {
        //         return _context.Dokumen
        //             .Include(p => p.KelompokDokumen)
        //             .Include(p => p.KelompokDokumen.JenisAtr)
        //             .OrderBy(p => p.KelompokDokumen.KodeJenisAtr)
        //             .ThenBy(p => p.KelompokDokumen.Nomor)
        //             .ThenBy(p => p.Nomor)
        //             .ToList();
        //     }
        // }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await selectListUtilities.Provinsi();
            ViewData["KabupatenKota"] = selectListUtilities.EmptyKabupatenKota;
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;

        private readonly PomeloDbContext _pomeloDbContext;
    }
}