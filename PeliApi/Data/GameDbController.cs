using PeliApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PeliApi.Data
{
    public class PongDbContext : DbContext
    {
        public DbSet<HighScore> Scores { get; set; }

        // This constructor allows us to pass in configuration options
        public PongDbContext(DbContextOptions<PongDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If the options haven't already been configured
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=PongDatabase.db");
            }
        }
    }
}
