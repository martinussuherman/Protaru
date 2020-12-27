using Microsoft.AspNetCore.Authorization;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrKsnT52
{
    [Authorize(Permissions.RtrKsnT52.Edit)]
    public class EditModel : Edit
    {
        public EditModel(PomeloDbContext context) : base(context)
        {
        }
    }
}