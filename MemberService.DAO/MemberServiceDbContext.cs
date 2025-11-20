
using Microsoft.EntityFrameworkCore;
using MemberService.BO.Entites;
using MemberService.BO.Common;

namespace MemberService.DAO
{
    public class MemberServiceDbContext : DbContext
    {
        public MemberServiceDbContext()
        {

        }
        public MemberServiceDbContext(DbContextOptions<MemberServiceDbContext> options) : base(options)
        {
        }

        private string CustomerConnectionString()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "35.232.237.179";
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3308";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "member_service";
            var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
            var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "123456";
            var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User Id={dbUser};Password={dbPass};SslMode=Required;";
            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(CustomerConnectionString(),
            new MySqlServerVersion(new Version(8, 0, 31)),
            mySqlOptions =>
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                ));

        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<PackageType> PackageTypes { get; set; } = null!;
        public DbSet<Membership> Memberships { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure EF Core does not create a table for the abstract BaseEntity
            modelBuilder.Ignore<BaseEntity>();
        }
    }
}