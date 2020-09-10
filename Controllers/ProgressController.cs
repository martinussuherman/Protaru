
using System.Linq;
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

        private readonly PomeloDbContext _context;
    }
}