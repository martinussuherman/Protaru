using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using Protaru.ViewComponents.Rtr;

namespace MonevAtr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RtrController : ControllerBase
    {
        public RtrController(PomeloDbContext context)
        {
            _context = context;
            _rtrUtilities = new RtrUtilities(context);
        }

        [HttpGet(nameof(RtrDetail))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RtrDetail(int? id)
        {
            _rtrDetail.Rtr = await _context.Atr
                .RtrIncludeAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Kode == id);
            _rtrDetail.KelompokDokumenList = await _rtrUtilities.LoadKelompokDokumenDanDokumen(
                _rtrDetail.Rtr.KodeJenisAtr);
            await _rtrUtilities.MergeRtrDokumenDenganKelompokDokumen(
                _rtrDetail.Rtr,
                _rtrDetail.KelompokDokumenList);

            JenisRtrEnum jenis = (JenisRtrEnum)_rtrDetail.Rtr.KodeJenisAtr;

            ViewModel result = new ViewModel
            {
                Title = ViewViewComponent.TitleByJenis(_rtrDetail.Rtr, jenis),
                NamaKabupatenKota = _rtrDetail.Rtr.DisplayNamaKabupatenKota,
                Nama = _rtrDetail.Rtr.Nama,
                StatusNomor = ViewViewComponent.StatusNomor(_rtrDetail.Rtr),
                Keterangan = _rtrDetail.Rtr.Keterangan
            };

            return Ok(result);
        }

        public class ViewModel
        {
            public string Title { get; set; }

            public string NamaKabupatenKota { get; set; }

            public string Nama { get; set; }

            public string StatusNomor { get; set; }

            public string Keterangan { get; set; }
        }

        private readonly PomeloDbContext _context;
        private readonly RtrUtilities _rtrUtilities;
        private readonly RtrDetail _rtrDetail = new RtrDetail();
    }
}