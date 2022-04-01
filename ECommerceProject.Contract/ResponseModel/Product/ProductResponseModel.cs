using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.ResponseModel.Product
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActiveCampaign { get; set; }
        public string CampaignName { get; set; }
        public double Limit { get; set; }
        public double CampaignPrice { get; set; }
        public decimal Price { get; set; }
        public double Stock { get; set; }
    }
}
