using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Contract.Common
{
    public class BaseResponseModel
    {
        public string Detail { get; set; }
        public bool IsError { get; set; }
    }
}
