using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.RequestModel.Campaign
{
    public class CampaignRequestModel
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public double Limit { get; set; }
        public double CampaignPrice { get; set; }
        public double TargetSalesCount { get; set; }
    }
}
