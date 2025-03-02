using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(OS => OS.ToString(), OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

            
            builder.OwnsOne(o => o.Address, a => a.WithOwner());


        }
    }
}
