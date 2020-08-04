using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Provinsi
{
    public class CreateModel : PageModel
    {
        public CreateModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Provinsi Provinsi { get; set; }

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

            _context.Provinsi.Add(Provinsi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}