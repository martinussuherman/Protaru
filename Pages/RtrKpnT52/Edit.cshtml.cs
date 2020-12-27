using Microsoft.AspNetCore.Authorization;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrKpnT52
{
    [Authorize(Permissions.RdtrKpnT52.Edit)]
    public class EditModel : Edit
    {
        public EditModel(PomeloDbContext context) : base(context)
        {
        }
    }
}