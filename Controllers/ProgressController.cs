
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;

namespace MonevAtr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        public ProgressController(PomeloDbContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(HomeSummary))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HomeSummary()
        {
            int progress = await _context.Atr
                .Where(c => c.SudahDirevisi == 0 && c.ProgressAtr.IsPerdaPerpres == 0)
                .CountAsync();
            int done = await _context.Atr
                .Where(c => c.SudahDirevisi == 0 && c.ProgressAtr.IsPerdaPerpres == 1)
                .CountAsync();

            var result = new
            {
                Progress = progress,
                Done = done
            };

            return Ok(result);
        }

        [HttpGet(nameof(DaerahMap))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DaerahMap()
        {
            return Ok(await RetrieveRtrDaerahMapData());
        }

        [HttpGet(nameof(ProgressT51))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProgressT51([FromQuery] int jenisRtr)
        {
            var penyusunan = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 1 || q.ProgressAtr.Nomor == 2)
                .AsNoTracking()
                .CountAsync();

            var rekomendasiGubernur = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 3)
                .AsNoTracking()
                .CountAsync();

            var persetujuanSubstansi = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 4 || q.ProgressAtr.Nomor == 5)
                .AsNoTracking()
                .CountAsync();

            var perda = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 6)
                .AsNoTracking()
                .CountAsync();

            var result = new
            {
                Penyusunan = penyusunan,
                RekomendasiGubernur = rekomendasiGubernur,
                PersetujuanSubstansi = persetujuanSubstansi,
                Perda = perda,
                Total = penyusunan + rekomendasiGubernur + persetujuanSubstansi + perda
            };

            return Ok(result);
        }

        [HttpGet(nameof(ProgressT52))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProgressT52([FromQuery] int jenisRtr)
        {
            var penyusunan = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor >= 1 && q.ProgressAtr.Nomor <= 5)
                .AsNoTracking()
                .CountAsync();

            var revisi = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 6 || q.ProgressAtr.Nomor == 7)
                .AsNoTracking()
                .CountAsync();

            var rekomendasiGubernur = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 8)
                .AsNoTracking()
                .CountAsync();

            var persetujuanSubstansi = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 9 || q.ProgressAtr.Nomor == 10)
                .AsNoTracking()
                .CountAsync();

            var perda = await _context.Atr
                .ByJenis((JenisRtrEnum)jenisRtr)
                .Where(q => q.ProgressAtr.Nomor == 11)
                .AsNoTracking()
                .CountAsync();

            var result = new
            {
                ProsesPK = penyusunan,
                Revisi = revisi,
                RekomendasiGubernur = rekomendasiGubernur,
                PersetujuanSubstansi = persetujuanSubstansi,
                Perda = perda,
                Total = penyusunan + revisi + rekomendasiGubernur + persetujuanSubstansi + perda
            };

            return Ok(result);
        }

        internal class ProvinsiMapData
        {
            [JsonIgnore]
            public Provinsi Provinsi { get; set; }

            public int Kode => Provinsi.Kode;

            public string Nama => Provinsi.Nama;

            public decimal Latitude => Provinsi.Lat;

            public decimal Longitude => Provinsi.Long;

            public int ProgressCount { get; set; }

            public int DoneCount { get; set; }

            public string ProgressLink { get; set; }

            public string DoneLink { get; set; }
        }

        private async Task<List<ProvinsiMapData>> RetrieveRtrDaerahMapData()
        {
            var progressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c => c.SudahDirevisi == 0 && c.KodeProvinsi != null)
                .GroupBy(c => new
                {
                    c.KodeProvinsi,
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new
                {
                    KodeProvinsi = r.Key.KodeProvinsi,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();

            List<Provinsi> provinsiList = await _context.Provinsi
                .Where(c => c.Kode != 0)
                .AsNoTracking()
                .ToListAsync();
            List<ProvinsiMapData> result = (provinsiList
                .Select(provinsi => new ProvinsiMapData
                {
                    Provinsi = provinsi
                }))
                .ToList();

            foreach (var data in progressList)
            {
                ProvinsiMapData mapData = result
                    .Find(c => c.Kode == data.KodeProvinsi);

                if (data.IsDone)
                {
                    mapData.DoneCount = data.Jumlah;
                }
                else
                {
                    mapData.ProgressCount = data.Jumlah;
                }

                mapData.ProgressLink = Url.Content($"~/Search/SearchResultDaerahByProgress?Rtr.Prov={data.KodeProvinsi}&Rtr.Perda=0");
                mapData.DoneLink = Url.Content($"~/Search/SearchResultDaerahByProgress?Rtr.Prov={data.KodeProvinsi}&Rtr.Perda=1");
            }

            return result;
        }

        private readonly PomeloDbContext _context;
    }
}