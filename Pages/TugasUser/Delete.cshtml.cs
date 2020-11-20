using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.TugasUser
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(PomeloDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Protaru.Models.TugasUser TugasUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TugasUser = await _context.TugasUser
                .FirstOrDefaultAsync(e => e.Id == id);

            if (TugasUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TugasUser = await _context.TugasUser.FindAsync((uint)id);

            if (TugasUser != null)
            {
                _context.TugasUser.Remove(TugasUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private readonly PomeloDbContext _context;
    }
}