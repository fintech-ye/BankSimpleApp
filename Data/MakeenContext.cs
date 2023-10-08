using Microsoft.EntityFrameworkCore;
using asp_simple.Models;
using Microsoft.Extensions.Options;
namespace asp_simple.Data
{
    public class MakeenContext : DbContext
    {
        public MakeenContext (DbContextOptions<MakeenContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("accounts");
        }
    }
}