using Microsoft.AspNetCore.Authorization;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrwT51
{
    [Authorize(Permissions.RtrwT51.Edit)]
    public class EditModel : Edit
    {
        public EditModel(PomeloDbContext context) : base(context)
        {
        }
    }
}