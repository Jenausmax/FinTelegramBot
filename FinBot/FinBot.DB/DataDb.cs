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

    }
}
