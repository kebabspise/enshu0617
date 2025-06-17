using enshu0617asp.Models;

namespace enshu0617asp.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Rooms.Any())
            {
                context.Rooms.Add(new Room
                {
                    Name = "Sample",
                    Size = 12,
                    Book = false
                });
                context.SaveChanges();
            }
        }
    }
}
