using ECommerceProject.Business.IService;
using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Contract.Common;
using ECommerceProject.Contract.RequestModel.Order;
using ECommerceProject.Contract.ResponseModel.Order;
using ECommerceProject.Core.Entities;
using ECommerceProject.Data.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(ECommerceDbContext dbContext, IUnitOfWork uow) : base(dbContext, uow)
        {

        }

        public async Task<BaseResponseModel> CreateOrder(OrderRequestModel model)
        {
            var result = new BaseResponseModel();

            double price = 0;

            if (model.IsCampaign)
            {
                var findCampaign = await _dbContext.Campaign.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefaultAsync();
                if (findCampaign == null)
                {
                    result.IsError = true;
                    result.Detail = "No campaigns found to be added to the order";
                    return result;
                }

                if (model.Quantity>findCampaign.Limit)
                {
                    result.IsError = true;
                    result.Detail = $"No more than {findCampaign.Limit} pieces can be ordered.";
                    return result;
                }

                model.Id = findCampaign.ProductId;
                price = findCampaign.CampaignPrice;
            }
            else
            {
                var findProduct = await _dbContext.Product.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefaultAsync();
                if (findProduct == null)
                {
                    result.IsError = true;
                    result.Detail = "No products found to be added to the order";
                    return result;
                }

                price = Convert.ToDouble(findProduct.Price);
            }

            var order = new Order();
            order.ProductId = model.Id;
            order.Quantity = model.Quantity;
            order.TotalAmount = price * model.Quantity;

            await _dbContext.Order.AddAsync(order);
            var saveChanges = await _uow.CommitAsync();

            if (saveChanges)
            {
                result.Detail = "Registration Successful";
            }
            else
            {
                result.IsError = true;
                result.Detail = "An error occurred";
            }

            return result;
        }

        public async Task<List<OrderResponseModel>> GetAllOrder()
        {

            var orderList = await _dbContext.Order.Where(x => !x.IsDeleted).Include(x => x.Product).ToListAsync();

            var resOrderList = orderList.Adapt<List<OrderResponseModel>>();

            return resOrderList;

        }
    }
}
