using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Core.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Duration { get; set; }
        public double Limit { get; set; }
        public double CampaignPrice { get; set; }
        public double TargetSalesCount { get; set; }
    }
}
