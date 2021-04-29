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
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryColor> CategoryColors { get; set; }
        public DbSet<Genre> Genres { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //unique, non-clustered index for ItemGenre relationships
            modelBuilder.Entity<ItemGenre>()
                .HasIndex(ig => new { ig.ItemId, ig.GenreId })
                .IsUnique()
                .IsClustered(false);
        }
        public override int SaveChanges() {
            var tracker = ChangeTracker;
            foreach (var entry in tracker.Entries()) {
                if (entry.Entity is FullAuditModel) {
                    var referenceEntity = entry.Entity as FullAuditModel;
                    switch (entry.State) {
                        case EntityState.Added:
                            referenceEntity.CreatedDate = System.DateTime.Now;
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            referenceEntity.LastModifiedDate = System.DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}