using BTRS.Models;
using Microsoft.EntityFrameworkCore;

namespace BTRS.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Passengers> passengers { get; set; }
        public DbSet<Trip> trips { get; set; }
        public DbSet<Bus> buses { get; set; }
        public DbSet<Admin> admins { get; set; }

        public DbSet<BTRS.Models.Passengers_Trip>? passengers_Trips { get; set; }
    }
}
