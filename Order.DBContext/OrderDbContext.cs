using Microsoft.EntityFrameworkCore;
using Order.Domain;
using System.Reflection;

namespace Order.DBContext
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            :base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<OrderMaster> OrderMasters {  get; set; }
        public virtual DbSet<OrderDetail> OrderDetails {  get; set; }
    }
}
