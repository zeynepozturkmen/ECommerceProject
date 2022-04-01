using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Core.Entities
{
    public class Order : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}
