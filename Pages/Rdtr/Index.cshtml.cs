using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rdtr
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
                return (from progressAtr in _context.ProgressAtr where progressAtr.KodeJenisAtr == 1 select progressAtr).ToList();
            }
        }

        public IActionResult OnGet()
        {
            ViewData["Provinsi"] = _context.SelectListProvinsi;
            ViewData["KabupatenKota"] = _context.EmptySelectListKabupatenKota;
            ViewData["Tahun"] = this.SelectListTahun;
            return Page();
        }

        private SelectList SelectListTahun
        {
            get
            {
                List<Models.Tahun> list = new List<Tahun>();
                list.Insert(0, new Tahun(0, "Pilih Tahun"));
                int tahunSekarang = DateTime.Today.Year;

                for (int tahun = tahunSekarang; tahun >= tahunSekarang - 10; tahun--)
                {
                    list.Add(new Tahun(tahun));
                }

                return new SelectList(list, "Value", "Text");
            }
        }

        private readonly MonevAtrDbContext _context;
    }
}