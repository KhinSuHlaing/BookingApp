using Microsoft.EntityFrameworkCore;

namespace BookingApp.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext>options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
    }
}
