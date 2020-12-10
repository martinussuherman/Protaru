using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.Models;

namespace Protaru.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinsekPersubController : ControllerBase
    {
        public LinsekPersubController(
            PomeloDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [HttpGet(nameof(LinsekAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LinsekAsync(
            [FromQuery] int tahun,
            [FromQuery] int bulan,
            [FromQuery] bool isRdtr)
        {
            List<ViewModel> list = await _context.PencarianRtr
                .LinsekByDokumen(isRdtr)
                .ViewModelListAsync(tahun, bulan);

            foreach (ViewModel item in list)
            {
                item.Url = await DetermineUrlAsync(item);
            }

            return Ok(list);
        }

        [HttpGet(nameof(PersubAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PersubAsync(
            [FromQuery] int tahun,
            [FromQuery] int bulan,
            [FromQuery] bool isRdtr)
        {
            List<ViewModel> list = await _context.PencarianRtr
                .PersubByDokumen(isRdtr)
                .ViewModelListAsync(tahun, bulan);

            foreach (ViewModel item in list)
            {
                item.Url = await DetermineUrlAsync(item);
            }

            return Ok(list);
        }

        public class ViewModel
        {
            public int Kode { get; set; }
            public int? Jenis { get; set; }
            public string Nama { get; set; }
            public string Lokasi { get; set; }
            public string Url { get; set; }
            public string Tanggal { get; set; }
        }

        private async Task<string> DetermineUrlAsync(ViewModel item)
        {
            string rtrName = ((JenisRtrEnum)item.Jenis).ToString();
            bool isCanEdit = false;

            switch (item.Jenis)
            {
                case (int)JenisRtrEnum.RdtrT51:
                    isCanEdit = (await _authorizationService.AuthorizeAsync(
                        User,
                        Permissions.RdtrT51.Edit)).Succeeded;
                    break;
                case (int)JenisRtrEnum.RdtrT52:
                    isCanEdit = (await _authorizationService.AuthorizeAsync(
                        User,
                        Permissions.RdtrT52.Edit)).Succeeded;
                    break;
                case (int)JenisRtrEnum.RtrwT50:
                    isCanEdit = (await _authorizationService.AuthorizeAsync(
                        User,
                        Permissions.RtrwT50.Edit)).Succeeded;
                    break;
                case (int)JenisRtrEnum.RtrwT51:
                    isCanEdit = (await _authorizationService.AuthorizeAsync(
                        User,
                        Permissions.RtrwT51.Edit)).Succeeded;
                    break;
                case (int)JenisRtrEnum.RtrwT52:
                    isCanEdit = (await _authorizationService.AuthorizeAsync(
                        User,
                        Permissions.RtrwT52.Edit)).Succeeded;
                    break;
            }

            string pageName = isCanEdit ? "Edit" : "View";

            return Url.Content($"~/{rtrName}/{pageName}/{item.Kode}");
        }

        private readonly PomeloDbContext _context;
        private readonly IAuthorizationService _authorizationService;
    }

    internal static class LinsekQueryExtensions
    {
        public static IQueryable<PencarianRtr> LinsekByDokumen(
            this IQueryable<PencarianRtr> query,
            bool isRdtr)
        {
            return isRdtr ?
                query.Where(q =>
                    q.KodeDokumen == 30 ||
                    q.KodeDokumen == 177
                ) :
                query.Where(q =>
                    q.KodeDokumen == 64 ||
                    q.KodeDokumen == 100 ||
                    q.KodeDokumen == 138
                );
        }

        public static IQueryable<PencarianRtr> PersubByDokumen(
            this IQueryable<PencarianRtr> query,
            bool isRdtr)
        {
            return isRdtr ?
                query.Where(q =>
                    q.KodeDokumen == 31 ||
                    q.KodeDokumen == 178
                ) :
                query.Where(q =>
                    q.KodeDokumen == 65 ||
                    q.KodeDokumen == 101 ||
                    q.KodeDokumen == 139
                );
        }

        public static IQueryable<LinsekPersubController.ViewModel> ToViewModel(
            this IQueryable<PencarianRtr> query)
        {
            return query
                .Select(c => new LinsekPersubController.ViewModel
                {
                    Kode = c.Kode,
                    Jenis = c.KodeJenisRtr,
                    Lokasi = c.DisplayNamaProvinsiKabupatenKota,
                    Nama = c.Nama,
                    Tanggal = c.DisplayTanggalDokumen
                });
        }

        public static async Task<List<LinsekPersubController.ViewModel>> ViewModelListAsync(
            this IQueryable<PencarianRtr> query,
            int tahun,
            int bulan)
        {
            return await query
                .Where(q => q.TahunDokumen == tahun && q.BulanDokumen == bulan)
                .Take(50)
                .AsNoTracking()
                .ToViewModel()
                .ToListAsync();
        }
    }
}