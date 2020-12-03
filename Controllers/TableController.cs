using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonevAtr.Models;

namespace Protaru.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        public TableController(PomeloDbContext context)
        {
            _context = context;
            selectListUtilities = new SelectListUtilities(context);
        }

        [HttpGet(nameof(Provinsi))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Provinsi()
        {
            return Ok(ConvertData(await selectListUtilities.ProvinsiAsync()));
        }

        [HttpGet(nameof(KabupatenKota))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> KabupatenKota(int kodeProvinsi)
        {
            return Ok(ConvertData(await selectListUtilities.KabupatenKotaAsync(kodeProvinsi)));
        }

        [HttpGet(nameof(Pulau))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Pulau()
        {
            return Ok(ConvertData(await selectListUtilities.PulauAsync()));
        }

        [HttpGet(nameof(Kawasan))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Kawasan()
        {
            return Ok(ConvertData(await selectListUtilities.KawasanAsync()));
        }

        [HttpGet(nameof(Progress))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Progress(int jenisRtr)
        {
            return Ok(ConvertData(await selectListUtilities.ProgressRtrAsync(jenisRtr, 0)));
        }

        [HttpGet(nameof(TahunPerdaPerpres))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TahunPerdaPerpres(int jenisRtr)
        {
            return Ok(ConvertData(await selectListUtilities.TahunAsyncOptional((JenisRtrEnum)jenisRtr)));
        }

        public class ViewModel
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        private List<ViewModel> ConvertData(IEnumerable<SelectListItem> source)
        {
            return source
                .Select(c => new ViewModel
                {
                    Value = c.Value,
                    Text = c.Text
                })
                .ToList();
        }
        private readonly SelectListUtilities selectListUtilities;
        private readonly PomeloDbContext _context;
    }
}