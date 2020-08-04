using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.JenisAtr
{
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.JenisAtr JenisRtr { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JenisAtr.Add(JenisRtr);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}