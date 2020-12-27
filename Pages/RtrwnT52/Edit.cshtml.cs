using Microsoft.AspNetCore.Authorization;
using MonevAtr.Models;
using Protaru.Identity;
using Protaru.PageModels;

namespace MonevAtr.Pages.RtrwnT52
{
    [Authorize(Permissions.RtrwnT52.Edit)]
    public class EditModel : Edit
    {
        public EditModel(PomeloDbContext context) : base(context)
        {
        }
    }
}