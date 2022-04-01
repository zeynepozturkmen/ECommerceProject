using ECommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Data.Configuration
{
    public class CampaignConfiguration : BaseConfiguration<Campaign>
    {
        public override void Configure(EntityTypeBuilder<Campaign> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Product)
            .WithMany(x => x.CampaignList)
            .HasForeignKey(x => x.ProductId);


        }
    }
}
