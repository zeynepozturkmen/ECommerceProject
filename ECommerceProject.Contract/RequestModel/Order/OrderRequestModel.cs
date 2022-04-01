using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.RequestModel.Order
{
    public class OrderRequestModel
    {
        public Guid Id { get; set; }
        public bool IsCampaign { get; set; }
        public int Quantity { get; set; }
    }
}
