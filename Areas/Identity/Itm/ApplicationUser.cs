using Microsoft.AspNetCore.Identity;

namespace Itm.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() : base() {}
        public ApplicationUser(string userName) : base(userName) {}
    }
}