using Inventory_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_API
{
    public class InventoryManagementDbContext : DbContext
    {
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }
        public DbSet<StockOrderItem> StockOrderItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many: Customer Order -> Customer Order Item
            modelBuilder.Entity<CustomerOrder>()
                .HasMany(co => co.CustomerOrderItems)
                .WithOne(coi => coi.CustomerOrder)
                .HasForeignKey(coi => coi.CustomerOrderId);

            // One to Many: Stock Order -> Stock Order Item
            modelBuilder.Entity<StockOrder>()
                .HasMany(so => so.StockOrderItems)
                .WithOne(soi => soi.StockOrder)
                .HasForeignKey(soi => soi.StockOrderId);

            // Many to One: Customer Order Item -> Product
            modelBuilder.Entity<CustomerOrderItem>()
                .HasOne(coi => coi.Product)
                .WithMany(p => p.CustomerOrderItems)
                .HasForeignKey(coi => coi.ProductId);

            // Many to One: Stock Order Item -> Product
            modelBuilder.Entity<StockOrderItem>()
                .HasOne(soi => soi.Product)
                .WithMany(p => p.StockOrderItems)
                .HasForeignKey(soi => soi.ProductId);

            // One to Many: Product Category -> Product
            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.CategoryId);

            // One to One: Stock Order -> Shipment
            modelBuilder.Entity<StockOrder>()
                .HasOne(so => so.Shipment)
                .WithOne(s => s.StockOrder)
                .HasForeignKey<Shipment>(s => s.StockOrderId);

            // Many to Many: Supplier -> Product
            modelBuilder.Entity<SupplierProduct>()
                .HasKey(sp => new { sp.SupplierId, sp.ProductId });

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Supplier)
                .WithMany(s => s.SupplierProducts)
                .HasForeignKey(sp => sp.SupplierId);

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SupplierProducts)
                .HasForeignKey(sp => sp.ProductId);
        }
    }
}
