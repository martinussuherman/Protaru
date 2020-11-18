using System.Linq;
using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.TugasUser
{
    public class EditModel : PageModel
    {
        public EditModel(
            PomeloDbContext context,
            IdentityDbContext identity)
        {
            _context = context;
            _identity = identity;
            selectListUtilities = new SelectListUtilities(context);
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

            ViewData["User"] = await selectListUtilities.Users(_identity);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TugasUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TugasUserExists(TugasUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TugasUserExists(uint id)
        {
            return _context.TugasUser.Any(e => e.Id == id);
        }

        private readonly SelectListUtilities selectListUtilities;
        private readonly PomeloDbContext _context;
        private readonly IdentityDbContext _identity;
    }
}