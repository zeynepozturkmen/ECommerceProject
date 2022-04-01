using ECommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Data.Configuration
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Product)
            .WithMany(x => x.OrderList)
            .HasForeignKey(x => x.ProductId);
        }
    }
}
