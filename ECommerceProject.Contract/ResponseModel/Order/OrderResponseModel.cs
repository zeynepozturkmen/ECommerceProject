using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.ResponseModel.Order
{
    public class OrderResponseModel
    {
        public DateTime RecordDate { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}
