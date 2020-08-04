using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public partial class MonevAtrDbContext : DbContext
    {
        public DbSet<PencarianRtr> PencarianRtr { get; set; }

        public DbSet<FilterPencarianRtr> FilterPencarianRtr { get; set; }
    }
}