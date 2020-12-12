using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace Protaru.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        public DashboardController(
            PomeloDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(RtrByJenisAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrByJenisAsync()
        {
            int[] data = new int[6];
            var group = await _context.Atr
                .GroupBy(c => c.KodeJenisAtr)
                .OrderBy(c => c.Key)
                .Select(c => new
                {
                    Rtr = (JenisRtrEnum)c.Key,
                    Jumlah = c.Count()
                })
                .ToListAsync();

            foreach (var item in group)
            {
                switch (item.Rtr)
                {
                    case JenisRtrEnum.RtrwnT51:
                    case JenisRtrEnum.RtrwnT52:
                        data[0] += item.Jumlah;
                        break;
                    case JenisRtrEnum.RtrPulauT51:
                    case JenisRtrEnum.RtrPulauT52:
                        data[1] += item.Jumlah;
                        break;
                    case JenisRtrEnum.RtrKsnT51:
                    case JenisRtrEnum.RtrKsnT52:
                        data[2] += item.Jumlah;
                        break;
                    case JenisRtrEnum.RtrKpnT51:
                    case JenisRtrEnum.RtrKpnT52:
                        data[3] += item.Jumlah;
                        break;
                    case JenisRtrEnum.RtrwT50:
                    case JenisRtrEnum.RtrwT51:
                    case JenisRtrEnum.RtrwT52:
                        data[4] += item.Jumlah;
                        break;
                    case JenisRtrEnum.RdtrT51:
                    case JenisRtrEnum.RdtrT52:
                        data[5] += item.Jumlah;
                        break;
                }
            }

            return Ok(new ViewModel
            {
                Label = _rtrLabel,
                Data = data
            });
        }

        [HttpGet(nameof(RtrCreatedAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrCreatedAsync([FromQuery] int tahun)
        {
            int[] data = new int[12];
            var group = await _context.LogUser
                .Where(c => c.JenisKegiatan < 1000 && c.Waktu.Year == tahun)
                .GroupBy(c => c.Waktu.Month)
                .OrderBy(c => c.Key)
                .Select(c => new
                {
                    Bulan = c.Key,
                    Jumlah = c.Count()
                })
                .ToListAsync();

            for (int index = 0; index < data.Length; index++)
            {
                var item = group.Find(c => c.Bulan == index + 1);

                if (item == null)
                {
                    continue;
                }

                data[index] = item.Jumlah;
            }

            return Ok(new ViewModel
            {
                Label = _monthLabel,
                Data = data
            });
        }

        [HttpGet(nameof(RtrUpdatedAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrUpdatedAsync([FromQuery] int tahun)
        {
            int[] data = new int[12];
            var group = await _context.LogUser
                .Where(c => c.JenisKegiatan > 1000 && c.Waktu.Year == tahun)
                .GroupBy(c => c.Waktu.Month)
                .OrderBy(c => c.Key)
                .Select(c => new
                {
                    Bulan = c.Key,
                    Jumlah = c.Count()
                })
                .ToListAsync();

            for (int index = 0; index < data.Length; index++)
            {
                var item = group.Find(c => c.Bulan == index + 1);

                if (item == null)
                {
                    continue;
                }

                data[index] = item.Jumlah;
            }

            return Ok(new ViewModel
            {
                Label = _monthLabel,
                Data = data
            });
        }

        public class ViewModel
        {
            public string[] Label { get; set; }
            public int[] Data { get; set; }
        }

        private readonly static string[] _rtrLabel =
        {
            "RTRWN",
            "PULAU/KEP",
            "KSN",
            "KPN",
            "RTRW",
            "RDTR"
        };
        private readonly static string[] _monthLabel =
        {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "Mei",
            "Jun",
            "Jul",
            "Ags",
            "Sep",
            "Okt",
            "Nov",
            "Des"
        };
        private readonly PomeloDbContext _context;
    }
}