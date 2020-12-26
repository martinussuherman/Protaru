using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrwnT51
{
    [Authorize(Permissions.RtrwnT51.Create)]
    public class CreateModel : Create
    {
        public CreateModel(PomeloDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return await DisplayPageAsync(id, JenisRtrEnum.RtrwnT51);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await SaveDataAsync(StatusRevisi.RegularT51);
        }
    }
}