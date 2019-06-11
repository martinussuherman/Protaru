using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Ajax
{
    public class FilterKabupatenKotaBerdasarkanProvinsiModel : PageModel
    {
        private readonly Models.MonevAtrDbContext _context;

        public FilterKabupatenKotaBerdasarkanProvinsiModel(Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public JsonResult OnGetAsync(int kodeProvinsi)
        {
            List<Models.KabupatenKota> listKabupatenKota = (from kabupatenKota in _context.KabupatenKota
                                                            where kabupatenKota.KodeProvinsi == kodeProvinsi
                                                            select kabupatenKota).ToList();
            listKabupatenKota.Insert(0, new Models.KabupatenKota(0, "Pilih Kabupaten/Kota"));
            return new JsonResult(new SelectList(listKabupatenKota, "Kode", "Nama"));
        }
    }
}