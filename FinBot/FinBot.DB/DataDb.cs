using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FinBot.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinBot.DB
{
    public class DataDb : DbContext
    {
        public DataDb(DbContextOptions<DataDb> options ) : base(options)
        {
           // Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consumption>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<Income>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<Consumption>()
                .HasOne(x =>x.Category)
                .WithMany()
                .HasForeignKey(x =>x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Income>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Incomes)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Incomes)
                .WithOne()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }
}
