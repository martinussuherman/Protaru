using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr
{
    public class SearchResultPageModel : PageModel
    {
        public AtrSearch Rtr { get; set; }

        public IPager<Atr> Hasil { get; set; }

        public string RegulationName { get; set; }

        public bool IsUseCreateForm { get; set; }

        public bool IsCanCreate { get; set; }

        public bool IsCanEdit { get; set; }

        public bool IsDisplayRegulation { get; set; }
    }
}