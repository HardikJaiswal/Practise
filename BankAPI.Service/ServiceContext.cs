using System;
using Microsoft.EntityFrameworkCore;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class ServiceContext : DbContext
    {
        //private string _connectionString;

        public DbSet<AccountHolder>? Accounts { get; set; }

        public DbSet<BankStaff>? Employees { get; set; }

        public DbSet<Currency>? Currencies { get; set; }

        public DbSet<Bank>? Banks { get; set; }

        public DbSet<Transaction>? Transactions { get; set; }

        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
        //{
        //    optionsBuilder.UseSqlServer("Data Source=GIDEON;Initial Catalog=ApiDemo;Integrated Security=True;Connect Timeout=30;");
        //}

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountHolder>()
                .HasAlternateKey(a => a.AccountNumber);
            modelBuilder.Entity<Currency>()
                .HasKey(c => new { c.Name, c.BankId });
        }

    }
}
