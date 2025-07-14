using Microsoft.EntityFrameworkCore;
using rentroute1.Models;

namespace rentroute1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AdminCars> AdminCars { get; set; }
        public DbSet<User> Register { get; set; } 


    }
}
