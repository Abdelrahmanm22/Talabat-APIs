using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repository.Configuration
{
    public class ProoductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ///hamsk el builder el hwa belnsbaly el product
            builder.HasOne(P => P.ProductBrand)
               .WithMany()
               .HasForeignKey(p => p.ProductBrandId);
            //.OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);
            //.OnDelete(DeleteBehavior.SetNull);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description).IsRequired();
            builder.Property(p=>p.PictureURL).IsRequired();
        }
    }
}
