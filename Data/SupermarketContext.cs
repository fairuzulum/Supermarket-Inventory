using Microsoft.EntityFrameworkCore;
using SupermarketInventory.Models;

namespace SupermarketInventory.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext(DbContextOptions<SupermarketContext> options) : base(options) {}

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Items)
                .WithOne(i => i.Warehouse)
                .HasForeignKey(i => i.WarehouseId);
        }
    }
}