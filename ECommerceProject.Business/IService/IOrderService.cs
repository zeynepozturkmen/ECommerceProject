using ECommerceProject.Contract.Common;
using ECommerceProject.Contract.RequestModel.Order;
using ECommerceProject.Contract.ResponseModel.Order;
using ECommerceProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.IService
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<List<OrderResponseModel>> GetAllOrder();
        Task<BaseResponseModel> CreateOrder(OrderRequestModel model);
    }
}