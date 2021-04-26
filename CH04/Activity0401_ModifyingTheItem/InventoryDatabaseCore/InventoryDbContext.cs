using Microsoft.EntityFrameworkCore;
using InventoryModels;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InventoryDatabaseCore {
    public class InventoryDbContext : DbContext {
        private static IConfigurationRoot _configuration;
        public InventoryDbContext() : base() { }
        public InventoryDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();
                var connection = _configuration.GetConnectionString("InventoryManager");
                optionsBuilder.UseSqlServer(connection);
            }
        }
    }
}