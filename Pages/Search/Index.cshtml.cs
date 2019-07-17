using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AtrSearch AtrSearch { get; set; }

        public IList<Models.ProgressAtr> ProgressAtr
        {
            get
            {
                return _context.ProgressAtr
                    .Include(p => p.JenisAtr)
                    .OrderBy(p => p.KodeJenisAtr)
                    .ThenBy(p => p.Nomor)
                    .ToList();
            }
        }

        public IList<Models.Dokumen> Dokumen
        {
            get
            {
                return _context.Dokumen
                    .Include(p => p.KelompokDokumen)
                    .Include(p => p.KelompokDokumen.JenisAtr)
                    .OrderBy(p => p.KelompokDokumen.KodeJenisAtr)
                    .ThenBy(p => p.KelompokDokumen.Nomor)
                    .ThenBy(p => p.Nomor)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await _context.GetSelectListProvinsi();
            ViewData["KabupatenKota"] = _context.EmptySelectListKabupatenKota;
            ViewData["Tahun"] = GetSelectListTahun();
            // ViewData["Dokumen"] = GetSelectListDokumen();
            return Page();
        }

        private SelectList GetSelectListTahun()
        {
            List<short> list = _context.Atr
                .OrderBy(a => a.Tahun)
                .Select(a => a.Tahun)
                .Distinct()
                .ToList();

            List<Models.Tahun> listHasil = new List<Tahun>();
            listHasil.Insert(0, new Tahun(0, "Pilih Tahun Perda"));

            foreach (short tahun in list)
            {
                listHasil.Add(new Tahun(tahun));
            }

            return new SelectList(listHasil, "Value", "Text");
        }

        private SelectList GetSelectListDokumen()
        {
            List<Models.Dokumen> list = _context.Dokumen
                .Include(p => p.KelompokDokumen)
                .Include(p => p.KelompokDokumen.JenisAtr)
                .OrderBy(p => p.KelompokDokumen.KodeJenisAtr)
                .ThenBy(p => p.KelompokDokumen.Nomor)
                .ThenBy(p => p.Nomor)
                .ToList();

            foreach (Models.Dokumen dokumen in list)
            {
                dokumen.Nama = dokumen.Nama + " - " + dokumen.KelompokDokumen.JenisAtr.Nama;
            }

            Models.Dokumen pilih = new Models.Dokumen
            {
                Kode = 0,
                Nama = "Pilih Dokumen"
            };

            list.Insert(0, pilih);

            return new SelectList(list, "Kode", "Nama");
        }

        private readonly MonevAtrDbContext _context;
    }
}