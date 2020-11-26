using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KritikSaran
{
    [Authorize]
    public class ListModel : PageModel
    {
        public ListModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IList<Protaru.Models.Saran> Saran { get; set; }

        public async Task OnGetAsync()
        {
            Saran = await _context.Saran
                .ToListAsync();
        }

        private readonly PomeloDbContext _context;
    }
}