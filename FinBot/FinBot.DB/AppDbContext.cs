using FinBot.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinBot.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

    }
}
