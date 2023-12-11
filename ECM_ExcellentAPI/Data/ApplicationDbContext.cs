using ECM_ExcellentAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ECM_ExcellentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }

        public DbSet<Product_Rate_History> ProductRates { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomersAddress { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrdersDetail { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set;}
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetail { get; set;}
        public DbSet<Product> Products { get; set; }
    }
}
