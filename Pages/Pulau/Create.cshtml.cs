using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Pulau
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Pulau Pulau { get; set; }

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

            _context.Pulau.Add(Pulau);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}