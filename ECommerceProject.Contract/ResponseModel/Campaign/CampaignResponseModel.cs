using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.ResponseModel.Campaign
{
    public class CampaignResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Product_Price { get; set; }
        public DateTime RecordDate { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public double Limit { get; set; }
        public double CampaignPrice { get; set; }
        public double TargetSalesCount { get; set; }
    }
}
