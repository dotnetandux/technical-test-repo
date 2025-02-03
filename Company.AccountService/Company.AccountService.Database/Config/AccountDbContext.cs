using Company.AccountService.Domain.Models;
using Company.AccountService.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;

namespace Company.AccountService.Database.Config
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<CurrentAccount>("Current")
                .HasValue<SavingsAccount>("Savings");
        }
    }
}
