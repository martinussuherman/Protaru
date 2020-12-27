using Microsoft.AspNetCore.Authorization;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrPulauT51
{
    [Authorize(Permissions.RtrPulauT51.Edit)]
    public class EditModel : Edit
    {
        public EditModel(PomeloDbContext context) : base(context)
        {
        }
    }
}