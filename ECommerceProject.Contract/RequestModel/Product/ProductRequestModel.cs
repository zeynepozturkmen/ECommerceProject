using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.RequestModel.Product
{
    public class ProductRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Stock { get; set; }
    }
}
