using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.KritikSaran
{
    [Authorize]
    public class ViewModel : PageModel
    {
        public ViewModel(PomeloDbContext context)
        {
            _context = context;
        }

        public Protaru.Models.Saran Saran { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Saran = await _context.Saran
                .FirstOrDefaultAsync(m => m.Id == id);

            return Saran == null ? NotFound() : (IActionResult)Page();
        }

        private readonly PomeloDbContext _context;
    }
}