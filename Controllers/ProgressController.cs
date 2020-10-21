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
            return Ok(await RetrieveRtrDaerahMapData(Url));
        }

        [HttpGet(nameof(NasionalMap))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NasionalMap()
        {
            return Ok(await RetrieveRtrNasionalMapData(Url));
        }

        [HttpGet(nameof(T51))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> T51([FromQuery] int jenisRtr)
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

        [HttpGet(nameof(T52))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> T52([FromQuery] int jenisRtr)
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

        [HttpGet(nameof(RtrwnT51))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrwnT51()
        {
            var penyusunan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT51)
                .Where(q => q.ProgressAtr.Nomor == 1)
                .AsNoTracking()
                .CountAsync();
            var penyepakatan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT51)
                .Where(q => q.ProgressAtr.Nomor == 2)
                .AsNoTracking()
                .CountAsync();
            var harmonisasi = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT51)
                .Where(q => q.ProgressAtr.Nomor == 3)
                .AsNoTracking()
                .CountAsync();
            var pembahasan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT51)
                .Where(q => q.ProgressAtr.Nomor == 4)
                .AsNoTracking()
                .CountAsync();
            var penetapan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT51)
                .Where(q => q.ProgressAtr.Nomor == 5)
                .AsNoTracking()
                .CountAsync();
            var result = new
            {
                PenyusunanMateriTeknis = penyusunan,
                PenyepakatanTpak = penyepakatan,
                HarmonisasiKemenkumham = harmonisasi,
                PembahasanSekretariat = pembahasan,
                PenetapanPresiden = penetapan,
                Total = penyusunan + penyepakatan + harmonisasi + pembahasan + penetapan
            };

            return Ok(result);
        }

        [HttpGet(nameof(RtrwnT52))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrwnT52()
        {
            var kajian = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 1)
                .AsNoTracking()
                .CountAsync();
            var pk = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 2)
                .AsNoTracking()
                .CountAsync();
            var penyusunan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 3)
                .AsNoTracking()
                .CountAsync();
            var penyepakatan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 4)
                .AsNoTracking()
                .CountAsync();
            var harmonisasi = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 5)
                .AsNoTracking()
                .CountAsync();
            var pembahasan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 6)
                .AsNoTracking()
                .CountAsync();
            var penetapan = await _context.Atr
                .ByJenis(JenisRtrEnum.RtrwnT52)
                .Where(q => q.ProgressAtr.Nomor == 7)
                .AsNoTracking()
                .CountAsync();
            var result = new
            {
                KajianPk = kajian,
                PenyusunanPk = pk,
                PenyusunanMateriTeknis = penyusunan,
                PenyepakatanTpak = penyepakatan,
                HarmonisasiKemenkumham = harmonisasi,
                PembahasanSekretariat = pembahasan,
                PenetapanPresiden = penetapan,
                Total = kajian + pk + penyusunan + penyepakatan + harmonisasi + pembahasan + penetapan
            };

            return Ok(result);
        }

        internal async Task<List<MapData>> DaerahMapList(IUrlHelper urlHelper)
        {
            return await RetrieveRtrDaerahMapData(urlHelper);
        }

        internal async Task<List<MapData>> NasionalMapList(IUrlHelper urlHelper)
        {
            return await RetrieveRtrNasionalMapData(urlHelper);
        }

        internal class MapData
        {
            [JsonIgnore]
            public Provinsi Provinsi { get; set; }

            [JsonIgnore]
            public Pulau Pulau { get; set; }

            [JsonIgnore]
            public Kawasan Kawasan { get; set; }

            public int Kode
            {
                get
                {
                    if (Provinsi != null)
                    {
                        return Provinsi.Kode;
                    }

                    if (Pulau != null)
                    {
                        return Pulau.Kode;
                    }

                    if (Kawasan != null)
                    {
                        return Kawasan.Kode;
                    }

                    return 0;
                }
            }

            public string Nama
            {
                get
                {
                    if (Provinsi != null)
                    {
                        return Provinsi.Nama;
                    }

                    if (Pulau != null)
                    {
                        return Pulau.Nama;
                    }

                    if (Kawasan != null)
                    {
                        return Kawasan.Nama;
                    }

                    return string.Empty;
                }
            }

            public decimal Latitude
            {
                get
                {
                    if (Provinsi != null)
                    {
                        return Provinsi.Lat;
                    }

                    if (Pulau != null)
                    {
                        return Pulau.Lat;
                    }

                    if (Kawasan != null)
                    {
                        return Kawasan.Lat;
                    }

                    return 0;
                }
            }

            public decimal Longitude
            {
                get
                {
                    if (Provinsi != null)
                    {
                        return Provinsi.Long;
                    }

                    if (Pulau != null)
                    {
                        return Pulau.Long;
                    }

                    if (Kawasan != null)
                    {
                        return Kawasan.Long;
                    }

                    return 0;
                }
            }

            public int ProgressCount { get; set; }

            public int DoneCount { get; set; }

            public string ProgressLink { get; set; }

            public string DoneLink { get; set; }

            public string Color { get; set; }
        }

        internal class ProgressData
        {
            public int? Kode { get; set; }

            public bool IsDone { get; set; }

            public int Jumlah { get; set; }
        }
        private async Task<List<MapData>> RetrieveRtrDaerahMapData(IUrlHelper urlHelper)
        {
            var progressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c =>
                    c.KodeJenisAtr >= 1 &&
                    c.KodeJenisAtr <= 5 &&
                    c.SudahDirevisi == 0 &&
                    c.KodeProvinsi != null)
                .GroupBy(c => new
                {
                    c.KodeProvinsi,
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new ProgressData
                {
                    Kode = r.Key.KodeProvinsi,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();
            List<MapData> result = await _context.Provinsi
                .Where(c => c.Kode != 0)
                .AsNoTracking()
                .Select(provinsi => new MapData
                {
                    Provinsi = provinsi
                })
                .ToListAsync();

            MergeData(
                urlHelper,
                progressList,
                result,
                "~/Search/SearchResultDaerahByProgress?Rtr.Prov={0}&Rtr.Perda=0",
                "~/Search/SearchResultDaerahByProgress?Rtr.Prov={0}&Rtr.Perda=1",
                "red");
            return result;
        }
        private async Task<List<MapData>> RetrieveRtrNasionalMapData(IUrlHelper urlHelper)
        {
            // Rtr Pulau
            var pulauProgressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c =>
                    (c.KodeJenisAtr == 6 ||
                    c.KodeJenisAtr == 7) &&
                    c.SudahDirevisi == 0)
                .GroupBy(c => new
                {
                    c.KodePulau,
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new ProgressData
                {
                    Kode = r.Key.KodePulau,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();
            List<MapData> pulauResult = await _context.Pulau
                .AsNoTracking()
                .Select(pulau => new MapData
                {
                    Pulau = pulau
                })
                .ToListAsync();

            MergeData(
                urlHelper,
                pulauProgressList,
                pulauResult,
                "~/RtrPulau/SearchResult?Rtr.Pulau={0}&Rtr.Perda=0",
                "~/RtrPulau/SearchResult?Rtr.Pulau={0}&Rtr.Perda=1",
                "orange");

            // Rtr Ksn
            var ksnProgressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c =>
                    (c.KodeJenisAtr == 8 ||
                    c.KodeJenisAtr == 9) &&
                    c.SudahDirevisi == 0)
                .GroupBy(c => new
                {
                    c.KodeKawasan,
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new ProgressData
                {
                    Kode = r.Key.KodeKawasan,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();
            List<MapData> ksnResult = await _context.Kawasan
                .AsNoTracking()
                .Select(kawasan => new MapData
                {
                    Kawasan = kawasan
                })
                .ToListAsync();

            MergeData(
                urlHelper,
                ksnProgressList,
                ksnResult,
                "~/RtrKsn/SearchResult?Rtr.Kawasan={0}&Rtr.Perda=0",
                "~/RtrKsn/SearchResult?Rtr.Kawasan={0}&Rtr.Perda=1",
                "magenta");

            // Rtrwn
            var rtrwnProgressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c =>
                    (c.KodeJenisAtr == 10 ||
                    c.KodeJenisAtr == 11) &&
                    c.SudahDirevisi == 0)
                .GroupBy(c => new
                {
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new ProgressData
                {
                    Kode = 0,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();
            List<MapData> rtrwnResult = new List<MapData>
            {
                new MapData
                {
                    Kawasan = new Kawasan
                    {
                        Kode = 0,
                        Nama = "Rtrwn",
                        Lat = -6.19M,
                        Long = 106.85M
                    }
                }
            };
            MergeData(
                urlHelper,
                rtrwnProgressList,
                rtrwnResult,
                "~/Rtrwn/SearchResult?Rtr.Perda=0",
                "~/Rtrwn/SearchResult?Rtr.Perda=1",
                "yellow");

            // Rtr Kpn
            var kpnProgressList = await _context.Atr
                .Include(c => c.ProgressAtr)
                .Where(c =>
                    (c.KodeJenisAtr == 12 ||
                    c.KodeJenisAtr == 13) &&
                    c.SudahDirevisi == 0)
                .GroupBy(c => new
                {
                    c.KodeProvinsi,
                    c.ProgressAtr.IsPerdaPerpres
                })
                .Select(r => new ProgressData
                {
                    Kode = r.Key.KodeProvinsi,
                    IsDone = (r.Key.IsPerdaPerpres == 1),
                    Jumlah = r.Count()
                })
                .ToListAsync();
            List<MapData> kpnResult = await _context.Provinsi
                .Where(c => c.Kode != 0)
                .AsNoTracking()
                .Select(provinsi => new MapData
                {
                    Provinsi = provinsi
                })
                .ToListAsync();

            MergeData(
                urlHelper,
                kpnProgressList,
                kpnResult,
                "~/RtrKpn/SearchResult?Rtr.Provinsi={0}&Rtr.Perda=0",
                "~/RtrKpn/SearchResult?Rtr.Provinsi={0}&Rtr.Perda=1",
                "red");

            List<MapData> result = new List<MapData>(pulauResult);
            result.AddRange(ksnResult);
            result.AddRange(rtrwnResult);
            result.AddRange(kpnResult);
            result.RemoveAll(c => c.DoneCount == 0 && c.ProgressCount == 0);
            return result;
        }
        private void MergeData(
            IUrlHelper urlHelper,
            List<ProgressData> progress,
            List<MapData> result,
            string progressLink,
            string doneLink,
            string color)
        {
            foreach (var mapData in result)
            {
                mapData.Color = color;
                mapData.ProgressLink = urlHelper.Content(string.Format(progressLink, mapData.Kode));
                mapData.DoneLink = urlHelper.Content(string.Format(doneLink, mapData.Kode));
            }

            foreach (var data in progress)
            {
                MapData mapData = result
                    .Find(c => c.Kode == data.Kode);

                if (data.IsDone)
                {
                    mapData.DoneCount = data.Jumlah;
                }
                else
                {
                    mapData.ProgressCount = data.Jumlah;
                }
            }
        }

        private readonly PomeloDbContext _context;
    }
}