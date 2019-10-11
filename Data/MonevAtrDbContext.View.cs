using Microsoft.EntityFrameworkCore;

namespace MonevAtr.Models
{
    public partial class MonevAtrDbContext : DbContext
    {
        public DbQuery<PencarianRtr> PencarianRtr { get; set; }

        public DbQuery<FilterPencarianRtr> FilterPencarianRtr { get; set; }
    }
}