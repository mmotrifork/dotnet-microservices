using Microsoft.EntityFrameworkCore;

namespace dotnet_consumer.Model
{
    public class PayloadContext : DbContext
    {
        public DbSet<Payload> Payloads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Configuring PayloadContext");
            // optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
            optionsBuilder.UseNpgsql("Host=db.dotnet_consumer;Database=postgres;Username=postgres;Password=postgres");
        }

        internal async Task<IEnumerable<Payload>> GetAllPayloadsAsync()
        {
            // This will fetch all Payloads and cast them to objects
            return await Payloads.ToListAsync().ConfigureAwait(false);
        }
    }
}