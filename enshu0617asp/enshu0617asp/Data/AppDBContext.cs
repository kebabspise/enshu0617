using enshu0617asp.Models;
using Microsoft.EntityFrameworkCore;

namespace enshu0617asp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }

    }
}