using Microsoft.EntityFrameworkCore;

namespace DemonstrateSearchFilter.Models
{
    public class BoxContext : DbContext
    {
        public BoxContext(DbContextOptions<BoxContext> options) : base(options) { }

        public DbSet<Box> Boxes { get; set; }
    }
}
