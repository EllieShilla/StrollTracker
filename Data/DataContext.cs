using Microsoft.EntityFrameworkCore;
using StrollTracker.Model;

namespace StrollTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TrackLocation> TrackLocation { get; set; }
    }
}
