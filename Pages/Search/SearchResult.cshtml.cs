using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itm.Identity;
using Itm.Misc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonevAtr.Models;
using P.Pager;
using Protaru.Models;

namespace MonevAtr.Pages.Search
{
    public class SearchResultModel : CustomPageModel
    {
        public SearchResultModel(
            PomeloDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Title = "Hasil Pencarian";
            PageTitle = "Pencarian";
        }

        [BindProperty]
        public AtrSearch Rtr { get; set; }

        public IPager<int> Pager { get; set; }

        public List<PencarianRtr> Hasil { get; set; } = new List<PencarianRtr>();

        public async Task<IActionResult> OnGetAsync(
            [FromQuery] AtrSearch rtr,
            [FromQuery] int page = 1)
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
                .AsNoTracking()
                .Select(f => f.Kode)
                .Distinct()
                .ToPagerList(page, PagerUrlHelper.ItemPerPage);

            List<PencarianRtr> temp = await _context.PencarianRtr
                .ByKodeList(Pager)
                .OrderBy(p => p.NamaProvinsi)
                .ThenBy(p => p.NamaProvinsiKabupatenKota)
                .ThenBy(p => p.NamaKabupatenKota)
                .AsNoTracking()
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
                case (int)JenisRtrEnum.RtrwT50:
                case (int)JenisRtrEnum.RtrwT51:
                    item.NamaStatusRevisi =
                        StatusRevisi.NamaStatusRevisiRegular(item.StatusRevisi);
                    break;
                case (int)JenisRtrEnum.RtrwT52:
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
                case (int)JenisRtrEnum.RdtrT51:
                    return "/RdtrT51";
                case (int)JenisRtrEnum.RdtrT52:
                    return "/RdtrT52";
                case (int)JenisRtrEnum.RtrwT50:
                    return "/RtrwT50";
                case (int)JenisRtrEnum.RtrwT51:
                    return "/RtrwT51";
                case (int)JenisRtrEnum.RtrwT52:
                    return "/RtrwT52";
                default:
                    break;
            }

            return String.Empty;
        }

        // TODO : check user permission
        private string AppendViewOrEdit() =>
            _currentUserId == -1 ? "/View" : "/Edit";

        private Task<ApplicationUser> GetCurrentUserAsync() =>
            _userManager.GetUserAsync(HttpContext.User);

        private async Task<int> GetCurrentUserId()
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (user == null)
            {
                return -1;
            }

            return user.Id;
        }

        private int _currentUserId;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly PomeloDbContext _context;
    }
}