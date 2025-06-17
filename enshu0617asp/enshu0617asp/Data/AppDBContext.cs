using enshu0617asp.Models;
using System.Collections.Generic;

namespace enshu0617asp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }

    }
