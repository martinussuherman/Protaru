using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itm.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Syncfusion.EJ2.DropDowns;

namespace MonevAtr.Pages
{
    public class IndexModel : CustomPageModel
    {
        public IndexModel(
            PomeloDbContext context)
        {
            _context = context;
        }

        public List<string> NamaRtr { get; set; }

        public AutoCompleteFieldSettings FieldSettings => new AutoCompleteFieldSettings
        {
            Text = "Nama",
            Value = "Nama"
        };

        public async Task<IActionResult> OnGet()
        {
            NamaRtr = await _context.Atr
                .OrderBy(c => c.Nama)
                .AsNoTracking()
                .Select(c => c.Nama )
                .Distinct()
                .ToListAsync();
            Title = "Kementerian Agraria dan Tata Ruang";
            return Page();
        }

        private readonly PomeloDbContext _context;
    }
}