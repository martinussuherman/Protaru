using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Kawasan
{
    [Authorize]
    public class EditModel : PageModel
    {
        public EditModel(MonevAtrDbContext context)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kawasan = await _context.Kawasan
                .FirstOrDefaultAsync(m => m.Kode == id);

            if (Kawasan == null)
            {
                return NotFound();
            }

            List<KawasanProvinsi> list = await _context.KawasanProvinsi
                .Where(k => k.KodeKawasan == id)
                .ToListAsync();

            // List<KawasanKabupatenKota> list = await _context.KawasanKabupatenKota
            //     .Where(k => k.KodeKawasan == id)
            //     .ToListAsync();

            Provinsi1 = Retrieve(list, 0);
            Provinsi2 = Retrieve(list, 1);
            Provinsi3 = Retrieve(list, 2);
            Provinsi4 = Retrieve(list, 3);
            Provinsi5 = Retrieve(list, 4);
            Provinsi6 = Retrieve(list, 5);
            Provinsi7 = Retrieve(list, 6);
            Provinsi8 = Retrieve(list, 7);
            Provinsi9 = Retrieve(list, 8);
            Provinsi10 = Retrieve(list, 9);
            Provinsi11 = Retrieve(list, 10);
            Provinsi12 = Retrieve(list, 11);

            ViewData["Provinsi"] = await _selectListUtilities.Provinsi();
            // ViewData["KabupatenKota"] = await _selectListUtilities.KabupatenKota();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Kawasan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(Kawasan.Kode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // List<KawasanKabupatenKota> list = await _context.KawasanKabupatenKota
            //     .Where(k => k.KodeKawasan == Kawasan.Kode)
            //     .ToListAsync();
            List<KawasanProvinsi> list = await _context.KawasanProvinsi
                .Where(k => k.KodeKawasan == Kawasan.Kode)
                .ToListAsync();

            _context.KawasanProvinsi.RemoveRange(list);
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

        private KawasanProvinsi Retrieve(List<KawasanProvinsi> list, int index)
        {
            if (index < list.Count)
            {
                return list[index];
            }

            return new KawasanProvinsi
            {
                KodeProvinsi = 0
            };
        }

        private void AddProvinsiToContext(KawasanProvinsi item)
        {
            if (item == null || item.KodeProvinsi == 0)
            {
                return;
            }

            item.KodeKawasan = Kawasan.Kode;
            _context.KawasanProvinsi.Add(item);
        }

        // private KawasanKabupatenKota Retrieve(List<KawasanKabupatenKota> list, int index)
        // {
        //     if (index < list.Count)
        //     {
        //         return list[index];
        //     }

        //     return new KawasanKabupatenKota
        //     {
        //         KodeKabupatenKota = 0
        //     };
        // }

        // private void AddKabupatenKotaToContext(KawasanKabupatenKota item)
        // {
        //     if (item == null || item.KodeKabupatenKota == 0)
        //     {
        //         return;
        //     }

        //     item.KodeKawasan = Kawasan.Kode;
        //     _context.KawasanKabupatenKota.Add(item);
        // }

        private bool Exists(int id)
        {
            return _context.Kawasan.Any(e => e.Kode == id);
        }

        private readonly SelectListUtilities _selectListUtilities;

        private readonly MonevAtrDbContext _context;
    }
}