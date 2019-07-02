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
                return (from progressAtr in _context.ProgressAtr where progressAtr.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi select progressAtr).ToList();
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
            List<short> list = (from atr in _context.Atr where atr.KodeJenisAtr == (int) JenisAtrEnum.RtrwRevisi select atr.Tahun)
                .Distinct()
                .ToList();

            List<Models.Tahun> listHasil = new List<Tahun>();
            listHasil.Insert(0, new Tahun(0, "Pilih Tahun"));

            foreach (short tahun in list)
            {
                listHasil.Add(new Tahun(tahun));
            }

            return new SelectList(listHasil, "Value", "Text");
        }

        private readonly MonevAtrDbContext _context;
    }
}