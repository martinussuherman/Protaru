using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
            _selectListUtilities = new SelectListUtilities(context);
        }

        [BindProperty]
        public Models.Kawasan Kawasan { get; set; }

        // [BindProperty]
        // public KawasanKabupatenKota KabupatenKota1 { get; set; }

        // [BindProperty]
        // public KawasanKabupatenKota KabupatenKota2 { get; set; }

        // [BindProperty]
        // public KawasanKabupatenKota KabupatenKota3 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi1 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi2 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi3 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi4 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi5 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi6 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi7 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi8 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi9 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi10 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi11 { get; set; }

        [BindProperty]
        public KawasanProvinsi Provinsi12 { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // ViewData["KabupatenKota"] = await _selectListUtilities.KabupatenKota();
            ViewData["Provinsi"] = await _selectListUtilities.Provinsi();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Kawasan.Add(Kawasan);
            await _context.SaveChangesAsync();

            // AddKabupatenKotaToContext(KabupatenKota1);
            // AddKabupatenKotaToContext(KabupatenKota2);
            // AddKabupatenKotaToContext(KabupatenKota3);

            AddProvinsiToContext(Provinsi1);
            AddProvinsiToContext(Provinsi2);
            AddProvinsiToContext(Provinsi3);
            AddProvinsiToContext(Provinsi4);
            AddProvinsiToContext(Provinsi5);
            AddProvinsiToContext(Provinsi6);
            AddProvinsiToContext(Provinsi7);
            AddProvinsiToContext(Provinsi8);
            AddProvinsiToContext(Provinsi9);
            AddProvinsiToContext(Provinsi10);
            AddProvinsiToContext(Provinsi11);
            AddProvinsiToContext(Provinsi12);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        // private void AddKabupatenKotaToContext(KawasanKabupatenKota item)
        // {
        //     if (item == null || item.KodeKabupatenKota == 0)
        //     {
        //         return;
        //     }

        //     item.KodeKawasan = Kawasan.Kode;
        //     _context.KawasanKabupatenKota.Add(item);
        // }

        private void AddProvinsiToContext(KawasanProvinsi item)
        {
            if (item == null || item.KodeProvinsi == 0)
            {
                return;
            }

            item.KodeKawasan = Kawasan.Kode;
            _context.KawasanProvinsi.Add(item);
        }

        private readonly SelectListUtilities _selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}