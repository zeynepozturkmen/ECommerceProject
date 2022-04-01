using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Stock { get; set; }
        public List<Campaign> CampaignList { get; set; } = new List<Campaign>();
        public List<Order> OrderList { get; set; } = new List<Order>();
        
    }
}
