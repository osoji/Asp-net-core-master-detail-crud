using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DBContext.EntityConfig
{
    public class OrderConfig : IEntityTypeConfiguration<OrderMaster>
    {
        public void Configure(EntityTypeBuilder<OrderMaster> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.OrderMasterId);
            builder.Property(o=>o.Address).IsRequired().HasMaxLength(255);
            builder.Property(o => o.OrderDate).IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder.Property(o => o.CustomerName).IsRequired().HasMaxLength(60);
            builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(12);
        }
    }
}
