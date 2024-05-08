using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.DBContext.EntityConfig
{
    public class DetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(d => d.OrderDetailId);
            builder.Property(d=>d.OrderMasterId).IsRequired();
            builder.Property(d=>d.Quantity).IsRequired();
            builder.Property(d=>d.ProductName).IsRequired().HasMaxLength(60);
            builder.Property(d=>d.Amount).IsRequired()
                .HasColumnType("decimal(8,2)");
        }
    }
}
