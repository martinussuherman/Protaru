using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.Search
{
    public class DaerahByProgressModel : PageModel
    {
        public DaerahByProgressModel(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        public AtrSearch Rtr { get; set; }

        public async Task<IActionResult> OnGetAsync(int provinsi)
        {
            Rtr = new AtrSearch
            {
                Prov = provinsi
            };
            
            ViewData["KabupatenKota"] = await selectListUtilities.KabupatenKota(provinsi);
            return Page();
        }

        private readonly SelectListUtilities selectListUtilities;

        private readonly PomeloDbContext _context;
    }
}