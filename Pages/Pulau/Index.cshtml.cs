using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Pulau
{
    public class IndexModel : PageModel
    {
        public IndexModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        public IList<Models.Pulau> Pulau { get; set; }

        public async Task OnGetAsync()
        {
            Pulau = await _context.Pulau
                .ToListAsync();
        }

        private readonly MonevAtrDbContext _context;
    }
}