using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrwT52
{
    [Authorize(Permissions.RtrwT52.Create)]
    public class CreateModel : Create
    {
        public CreateModel(PomeloDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return await DisplayPageAsync(id, JenisRtrEnum.RtrwT52);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await SaveDataAsync(StatusRevisi.RevisiT52);
        }
    }
}