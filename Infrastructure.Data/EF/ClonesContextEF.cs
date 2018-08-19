using Infrastructure.Data.Entities;
using System.Data.Entity;

namespace Infrastructure.Data.EF
{
    public class ClonesContextEF : DbContext
    {
        public ClonesContextEF(string connectionString)
            : base(connectionString) { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>().Property(r => r.Price).HasPrecision(18, 4);
            modelBuilder.Entity<Resource>().Property(r => r.PriceBase).HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
