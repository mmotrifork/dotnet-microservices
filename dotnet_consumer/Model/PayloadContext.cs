using Microsoft.EntityFrameworkCore;

namespace dotnet_consumer.Model
{
    public class PayloadContext : DbContext
    {
        public DbSet<Payload> Payloads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Configuring PayloadContext");

            // Construct the connection string using environment variables
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Host={host};Database={database};Username={username};Password={password}";

            optionsBuilder.UseNpgsql(connectionString);
        }

        internal async Task<IEnumerable<Payload>> GetAllPayloadsAsync()
        {
            // This will fetch all Payloads and cast them to objects
            return await Payloads.ToListAsync().ConfigureAwait(false);
        }
    }
}