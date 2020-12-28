using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rtr
{
    [Authorize]
    public class FixModel : PageModel
    {
        public FixModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await FixItemListAsync(await RtrwKabKotaListAsync());
            await FixItemListAsync(await RtrwProvinsiListAsync());
            await FixItemListAsync(await RdtrKabKotaListAsync());
            await FixItemListAsync(await RdtrProvinsiListAsync());

            return Page();
        }

        public class FixData
        {
            public int? KodeLama { get; set; }
            public int? KodeBaru { get; set; }
        }

        private async Task<List<FixData>> RtrwKabKotaListAsync()
        {
            return await _context.RtrwT5152Kabkota
                .Select(c => new FixData
                {
                    KodeBaru = c.KodeBaru,
                    KodeLama = c.KodeLama
                })
                .AsNoTracking()
                .ToListAsync();
        }
        private async Task<List<FixData>> RtrwProvinsiListAsync()
        {
            return await _context.RtrwT5152Provinsi
                .Select(c => new FixData
                {
                    KodeBaru = c.KodeBaru,
                    KodeLama = c.KodeLama
                })
                .AsNoTracking()
                .ToListAsync();
        }
        private async Task<List<FixData>> RdtrKabKotaListAsync()
        {
            return await _context.RdtrT5152Kabkota
                .Select(c => new FixData
                {
                    KodeBaru = c.KodeBaru,
                    KodeLama = c.KodeLama
                })
                .AsNoTracking()
                .ToListAsync();
        }
        private async Task<List<FixData>> RdtrProvinsiListAsync()
        {
            return await _context.RdtrT5152Provinsi
                .Select(c => new FixData
                {
                    KodeBaru = c.KodeBaru,
                    KodeLama = c.KodeLama
                })
                .AsNoTracking()
                .ToListAsync();
        }
        private async Task FixItemListAsync(List<FixData> list)
        {
            foreach (FixData data in list)
            {
                await FixLinkAsync(data);
            }
        }
        private async Task FixLinkAsync(FixData item)
        {
            Models.Atr previous = new Models.Atr()
            {
                Kode = (int)item.KodeLama,
                NextRtr = item.KodeBaru
            };
            Models.Atr next = new Models.Atr()
            {
                Kode = (int)item.KodeBaru,
                PreviousRtr = item.KodeLama
            };

            _context.Atr.Attach(previous);
            _context.Entry(previous)
                .Property(c => c.NextRtr)
                .IsModified = true;
            _context.Atr.Attach(next);
            _context.Entry(next)
                .Property(c => c.PreviousRtr)
                .IsModified = true;
            await _context.SaveChangesAsync();
        }

        private readonly PomeloDbContext _context;
    }
}