using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.ProgressAtr
{
    public class CreateModel : PageModel
    {
        public CreateModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.ProgressAtr ProgressAtr { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["JenisAtr"] = await _context.GetSelectListJenisAtr();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProgressAtr.Add(this.ProgressAtr);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private readonly MonevAtrDbContext _context;
    }
}