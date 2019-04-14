using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext()
        {
        }

        public PhoneBookDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<PhoneUser> Users { get; set; }

        public DbSet<OutgoingCall> OutgoingCalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
