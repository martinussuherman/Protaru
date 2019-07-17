using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.RtrwRevisi
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
                    .Where(p => p.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi)
                    .OrderBy(p => p.Nomor)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Provinsi"] = await _context.GetSelectListProvinsi();
            ViewData["KabupatenKota"] = _context.EmptySelectListKabupatenKota;
            ViewData["Tahun"] = GetSelectListTahun();
            return Page();
        }

        private SelectList GetSelectListTahun()
        {
            List<short> list = _context.Atr
                .Where(a => a.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi)
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

        private readonly MonevAtrDbContext _context;
    }
}