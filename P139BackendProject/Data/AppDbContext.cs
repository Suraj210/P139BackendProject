using Microsoft.EntityFrameworkCore;
using P139BackendProject.Models;
using System.Drawing.Drawing2D;

namespace P139BackendProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advert>().HasQueryFilter(m => !m.SoftDeleted);
        }
    }
}
