using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KabupatenKota
{
    public class IndexModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public IndexModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<MonevAtr.Models.KabupatenKota> KabupatenKota { get;set; }

        public async Task OnGetAsync()
        {
            KabupatenKota = await _context.KabupatenKota
                .Include(k => k.Provinsi).ToListAsync();
        }
    }
}
