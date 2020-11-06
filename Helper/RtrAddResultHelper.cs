using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonevAtr;
using MonevAtr.Models;
using P.Pager;

namespace Protaru.Helpers
{
    public class RtrAddResultHelper
    {
        public enum AddType
        {
            All,
            Rdtr,
            Rtrw
        }

        public RtrAddResultHelper(PomeloDbContext context)
        {
            _context = context;
        }

        public async Task<IPager<Atr>> PagerListAsync(AtrSearch rtr, AddType type, int page)
        {
            AddJenisFilter(rtr, type);

            List<Atr> result = await _context.Atr
                .DaerahByProgressNoTracking(rtr)
                .ToListAsync();

            if (rtr.Perda == 1)
            {
                await AddResultAsync(result, rtr, type);
            }

            return result.ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private async Task AddResultAsync(List<Atr> result, AtrSearch rtr, AddType type)
        {
            List<int> combined = new List<int>();

            if (type == AddType.All || type == AddType.Rtrw)
            {
                combined.AddRange(await _context.RtrwT5152Kabkota
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
                combined.AddRange(await _context.RtrwT5152Provinsi
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
            }

            if (type == AddType.All || type == AddType.Rdtr)
            {
                combined.AddRange(await _context.RdtrT5152Kabkota
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
                combined.AddRange(await _context.RdtrT5152Provinsi
                    .Where(c => c.IsPerdaPerpresLama == 1 && c.IsPerdaPerpresBaru == 0)
                    .Select(c => c.KodeLama)
                    .ToListAsync());
            }

            var addedResult = await _context.Atr
                .ByJenisList(rtr)
                .ByProvinsi(rtr.Prov, rtr.KabKota)
                .ByKabupatenKota(rtr.KabKota)
                .ByTahun(rtr.Tahun)
                .ByNama(rtr.Nama)
                .ByNomor(rtr.Nomor)
                .Where(c => combined.Contains(c.Kode))
                .RtrInclude()
                .AsNoTracking()
                .ToListAsync();

            result.AddRange(addedResult);
        }
        private void AddJenisFilter(AtrSearch rtr, AddType type)
        {
            if (type == AddType.All || type == AddType.Rtrw)
            {
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT50);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT52);
            }

            if (type == AddType.All || type == AddType.Rdtr)
            {
                rtr.JenisList.Add((int)JenisRtrEnum.RdtrT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RdtrT52);
            }
        }

        private readonly PomeloDbContext _context;
    }
}