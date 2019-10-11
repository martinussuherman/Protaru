using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonevAtr.Models;

namespace MonevAtr.Pages.RdtrT51
{
    public class EditResultModel : PageModel
    {
        public EditResultModel(MonevAtrDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Atr Atr { get; set; }

        [BindProperty]
        public List<Models.AtrDokumen> AtrDokumenList { get; set; }

        // [BindProperty]
        // public List<IFormFile> UploadFile { get; set; }

        public IActionResult OnPost()
        {
            FixUploadFiles(HttpContext);
            return Page();
        }

        private void FixUploadFiles(HttpContext httpContext)
        {
            int count = 0;

            foreach (Models.AtrDokumen dokumen in this.AtrDokumenList)
            {
                string propertyName = $"AtrDokumen[{count++}].UploadFile";
                // dokumen.UploadFile = httpContext.Request.Form.Files
                //     .Where(i => propertyName.Equals(i.Name, StringComparison.InvariantCultureIgnoreCase))
                //     .FirstOrDefault();
            }
        }

        private readonly MonevAtrDbContext _context;
    }
}