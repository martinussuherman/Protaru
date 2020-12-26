using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace Protaru.PageModels
{
    public class Create : PageModel
    {
        public Create(PomeloDbContext context)
        {
            _context = context;
            _rtrUtilities = new RtrUtilities(context);
        }

        [BindProperty]
        public Atr Rtr { get; set; }

        public async Task<IActionResult> DisplayPageAsync(int? id, JenisRtrEnum jenis)
        {
            Rtr = await RetrieveRtrAsync(id);
            Rtr.PreviousRtr = id;
            Rtr.KodeJenisAtr = (int)jenis;
            return Page();
        }

        public async Task<IActionResult> SaveDataAsync(StatusRevisi revisi)
        {
            ValidatePropertiesOnPost();
            Rtr.StatusRevisi = (sbyte)revisi.Kode;
            await _rtrUtilities.SaveRtr(Rtr, User, EntityState.Added);

            if (Rtr.PreviousRtr != null)
            {
                await UpdatePreviousRtrLink(Rtr);
            }

            return RedirectToPage("./Index");
        }

        private async Task<Atr> RetrieveRtrAsync(int? id)
        {
            if (id is null)
            {
                return new Atr();
            }

            return await _context.Atr
                .RtrIncludeAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);
        }
        private void ValidatePropertiesOnPost()
        {
            if (Rtr.KodeProvinsi == 0)
            {
                Rtr.KodeProvinsi = null;
            }

            if (Rtr.KodeKabupatenKota == 0)
            {
                Rtr.KodeKabupatenKota = null;
            }
            // else if (Rtr.KodeKabupatenKota != null)
            // {
            //     Rtr.KodeProvinsi = null;
            // }

            if (Rtr.KodePulau == 0)
            {
                Rtr.KodePulau = null;
            }

            if (Rtr.KodeKawasan == 0)
            {
                Rtr.KodeKawasan = null;
            }
        }
        private async Task UpdatePreviousRtrLink(Atr currentRtr)
        {
            Atr previous = new Atr()
            {
                Kode = (int)currentRtr.PreviousRtr,
                NextRtr = currentRtr.Kode,
                SudahDirevisi = 1
            };

            _context.Atr.Attach(previous);
            _context.Entry(previous)
                .Property(c => c.SudahDirevisi)
                .IsModified = true;
            _context.Entry(previous)
                .Property(c => c.NextRtr)
                .IsModified = true;
            await _context.SaveChangesAsync();
        }

        private readonly RtrUtilities _rtrUtilities;
        private readonly PomeloDbContext _context;
    }
}
