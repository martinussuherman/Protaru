using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.TugasUser
{
    public class IndexModel : PageModel
    {
        public IndexModel(PomeloDbContext context)
        {
            _context = context;
        }

        public IPager<Protaru.Models.TugasUser> TugasUser { get; set; }

        public void OnGet([FromQuery] int page)
        {
            TugasUser = _context.TugasUser
                .OrderByDescending(k => k.BatasWaktu)
                .ThenBy(k => k.User)
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private readonly PomeloDbContext _context;
    }
}