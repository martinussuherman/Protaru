using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RdtrT51
{
    [Authorize(Permissions.RdtrT51.Create)]
    public class CreateModel : Create
    {
        public CreateModel(PomeloDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return await DisplayPageAsync(id, JenisRtrEnum.RdtrT51);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await SaveDataAsync(StatusRevisi.RegularT51);
        }
    }
}