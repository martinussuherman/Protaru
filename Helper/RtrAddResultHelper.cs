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
        public RtrAddResultHelper(PomeloDbContext context)
        {
            _context = context;
        }

        public async Task<IPager<Atr>> PagerListAsync(
            AtrSearch rtr,
            JenisRtrEnum jenis,
            int page)
        {
            AddJenisFilter(rtr, jenis);

            List<Atr> result = await _context.Atr
                .DaerahByProgressNoTracking(rtr, jenis)
                .ToListAsync();

            if (rtr.Perda == 1 || rtr.Perda == 1000)
            {
                await AddResultAsync(result, rtr, jenis);
            }

            return result.ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }

        private async Task AddResultAsync(List<Atr> result, AtrSearch rtr, JenisRtrEnum jenis)
        {
            List<int> combined = new List<int>();

            if (jenis == JenisRtrEnum.All || jenis == JenisRtrEnum.Daerah || jenis == JenisRtrEnum.Rtrw)
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

            if (jenis == JenisRtrEnum.All || jenis == JenisRtrEnum.Daerah || jenis == JenisRtrEnum.Rdtr)
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

            // TODO: add ksn, kpn, rtrwn, pulau to combined

            var addedResult = await _context.Atr
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
            result
                .OrderBy(a => a.Provinsi.Nama)
                .ThenBy(a => a.KabupatenKota.Provinsi.Nama)
                .ThenBy(a => a.KabupatenKota.Nama);
        }
        private void AddJenisFilter(AtrSearch rtr, JenisRtrEnum jenis)
        {
            if (jenis == JenisRtrEnum.All || jenis == JenisRtrEnum.Daerah || jenis == JenisRtrEnum.Rtrw)
            {
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT50);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwT52);
            }

            if (jenis == JenisRtrEnum.All || jenis == JenisRtrEnum.Daerah || jenis == JenisRtrEnum.Rdtr)
            {
                rtr.JenisList.Add((int)JenisRtrEnum.RdtrT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RdtrT52);
            }

            if (jenis == JenisRtrEnum.All || jenis == JenisRtrEnum.Nasional)
            {
                rtr.JenisList.Add((int)JenisRtrEnum.RtrKpnT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrKpnT52);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrKsnT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrKsnT52);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrPulauT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrPulauT52);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwnT51);
                rtr.JenisList.Add((int)JenisRtrEnum.RtrwnT52);
            }
        }

        private readonly PomeloDbContext _context;
    }
}