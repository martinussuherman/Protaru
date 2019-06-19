using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Pages.Rdtr
{
    public class SearchResultModel : PageModel
    {
        private readonly MonevAtrDbContext _context;

        public SearchResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AtrSearch AtrSearch { get; set; }

        public List<Models.Atr> HasilPencarian { get; set; } = new List<Models.Atr>();

        public IActionResult OnPost()
        {
            IQueryable<Models.Atr> query = (from atr in _context.Atr where atr.KodeJenisAtr == 1 select atr);

            if (this.AtrSearch.KodeProvinsi != 0 && this.AtrSearch.KodeKabupatenKota == 0)
            {
                query = query.Where(q => q.KodeProvinsi == this.AtrSearch.KodeProvinsi);
            }

            if (this.AtrSearch.KodeKabupatenKota != 0)
            {
                query = query.Where(q => q.KodeKabupatenKota == this.AtrSearch.KodeKabupatenKota);
            }

            if (this.AtrSearch.Tahun != 0)
            {
                query = query.Where(q => q.Tahun == this.AtrSearch.Tahun);
            }

            if (!string.IsNullOrEmpty(this.AtrSearch.Nama))
            {
                string pattern = this.AtrSearch.Nama + "%";
                query = query.Where(q => EF.Functions.Like(q.Nama, pattern));
            }

            if (!string.IsNullOrEmpty(this.AtrSearch.Nomor))
            {
                string pattern = this.AtrSearch.Nomor + "%";
                query = query.Where(q => EF.Functions.Like(q.Nomor, pattern));
            }

            this.HasilPencarian = query.ToList();

            // var query = from r in db.requests 
            // select new ProductReqNoDate
            //                    {
            //                        departmant= r.departmant,
            //                        reqNo = r.reqNo ,
            //                        reqDate = r.reqDate ,
            //                        prdctName= stringCutter((from p in db.products 
            //                       where p.reqNo == r.reqNo select p.prdctName).FirstOrDefault())
            //                    }
            // if (!string.IsNullOrEmpty(firstDate) && !string.IsNullOrEmpty(lastDate))
            // {
            //         DateTime dtfirstDate = Convert.ToDateTime(firstDate);
            //         DateTime dtlastDate = Convert.ToDateTime(lastDate);
            //         return query.Where(r=> r.reqDate <= dtlastDate && r.reqDate >= dtfirstDate)
            //                     .ToList();
            //  }            
            // return RedirectToPage("./Index");
            return Page();
        }
    }
}