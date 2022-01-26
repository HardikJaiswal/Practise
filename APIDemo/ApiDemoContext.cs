using Microsoft.EntityFrameworkCore;
using APIDemo.Models;

namespace APIDemo
{
    public class ApiDemoContext : DbContext
    {
        public ApiDemoContext()
        {

        }

        public ApiDemoContext(DbContextOptions<ApiDemoContext> options): base(options) { }

        public DbSet<AccountHolder> Accounts { get; set; }
        
        public DbSet<BankStaff> Staffs { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountHolder>()
                .HasAlternateKey(a => a.AccountNumber);
            modelBuilder.Entity<Currency>()
                .HasKey(c => new { c.Name, c.BankId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GIDEON;Initial Catalog=ApiDemo;Integrated Security=True");
        }
    }
}
