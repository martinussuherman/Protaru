using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class IndexModel : PageModel
    {
        private readonly MonevAtr.Models.MonevAtrDbContext _context;

        public IndexModel(MonevAtr.Models.MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<MonevAtr.Models.ProgressAtr> ProgressAtr { get;set; }

        public async Task OnGetAsync()
        {
            ProgressAtr = await _context.ProgressAtr
                .Include(p => p.JenisAtr).ToListAsync();
        }
    }
}
