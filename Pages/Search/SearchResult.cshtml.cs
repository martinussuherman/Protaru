using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;

namespace MonevAtr.Pages.Search
{
    public class SearchResultModel : PageModel
    {
        public SearchResultModel(
            MonevAtrDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IPager<int> Pager { get; set; }

        public List<PencarianRtr> Hasil { get; set; } = new List<PencarianRtr>();

        public async Task<IActionResult> OnGetAsync([FromQuery] AtrSearch rtr, [FromQuery] int page = 1)
        {
            Rtr = rtr;
            Pager = _context.FilterPencarianRtr
                .ByJenisList(Rtr)
                .ByProvinsi(Rtr)
                .ByKabupatenKota(Rtr)
                .ByTahunRekomendasiGubernurList(Rtr)
                .ByTahunPermohonanPersetujuanSubstansiList(Rtr)
                .ByTahunMasukLoketList(Rtr)
                .ByTahunRapatLintasSektorList(Rtr)
                .ByTahunPersetujuanSubstansiList(Rtr)
                .ByTahunPerdaList(Rtr)
                .ByFasilitasKegiatanList(Rtr)
                .Select(f => f.Kode)
                .Distinct()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);

            List<PencarianRtr> temp = await _context.PencarianRtr
                .ByKodeList(Pager)
                .OrderBy(p => p.NamaProvinsi)
                .ThenBy(p => p.NamaProvinsiKabupatenKota)
                .ThenBy(p => p.NamaKabupatenKota)
                .ToListAsync();

            Hasil = temp
                .Distinct(new KodeComparer<PencarianRtr>())
                .ToList();

            _currentUserId = await GetCurrentUserId();
            Hasil.ForEach(SetDetailPath);
            Hasil.ForEach(SetNamaStatusRevisi);

            return Page();
        }

        private void SetNamaStatusRevisi(PencarianRtr item)
        {
            switch (item.KodeJenisRtr)
            {
                case (int) JenisRtrEnum.RtrwT50:
                case (int) JenisRtrEnum.RtrwT51:
                    item.NamaStatusRevisi =
                        StatusRevisi.NamaStatusRevisiRegular(item.StatusRevisi);
                    break;
                case (int) JenisRtrEnum.RtrwT52:
                    item.NamaStatusRevisi =
                        StatusRevisi.NamaStatusRevisiRevisi(item.StatusRevisi);
                    break;
                default:
                    item.NamaStatusRevisi = String.Empty;
                    break;
            }
        }

        private void SetDetailPath(PencarianRtr item) =>
            item.DetailPath =
            RetrieveDetailPage(item.KodeJenisRtr.Value) +
            AppendViewOrEdit();

        private string RetrieveDetailPage(int kodeJenisRtr)
        {
            switch (kodeJenisRtr)
            {
                case (int) JenisRtrEnum.RdtrT51:
                    return "/RdtrT51";
                case (int) JenisRtrEnum.RdtrT52:
                    return "/RdtrT52";
                case (int) JenisRtrEnum.RtrwT50:
                    return "/RtrwT50";
                case (int) JenisRtrEnum.RtrwT51:
                    return "/RtrwT51";
                case (int) JenisRtrEnum.RtrwT52:
                    return "/RtrwT52";
                default:
                    break;
            }

            return String.Empty;
        }

        private string AppendViewOrEdit() =>
            String.IsNullOrEmpty(_currentUserId) ? "/View" : "/Edit";

        private Task<IdentityUser> GetCurrentUserAsync() =>
            _userManager.GetUserAsync(HttpContext.User);

        private async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private string _currentUserId;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly MonevAtrDbContext _context;
    }
}