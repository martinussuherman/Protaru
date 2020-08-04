using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itm.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}