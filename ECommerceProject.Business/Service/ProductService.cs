using ECommerceProject.Business.IService;
using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Contract.RequestModel.Product;
using ECommerceProject.Contract.ResponseModel.Product;
using ECommerceProject.Core.Entities;
using ECommerceProject.Data.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.Business.Service
{
    public class ProductService : BaseService<Product> , IProductService
    {
        public ProductService(ECommerceDbContext dbContext, IUnitOfWork uow) : base(dbContext, uow)
        {

        }
        public async Task<List<ProductResponseModel>> GetAllProduct()
        {

            var productList = await _dbContext.Product.Where(x => !x.IsDeleted).ToListAsync();
            var data = productList.Adapt<List<ProductResponseModel>>();

            foreach (var item in data)
            {
                var findCampaignList = await _dbContext.Campaign.Where(x => x.ProductId == item.Id && !x.IsDeleted && x.RecordDate.Date== DateTime.Now.Date).ToListAsync();

                foreach (var campaign in findCampaignList)
                {
                    var diff = DateTime.Now.Hour - campaign.RecordDate.Hour;
                    if (diff <= campaign.Duration)
                    {
                        item.IsActiveCampaign = true;
                        item.CampaignName = campaign.Name;
                        item.Limit = campaign.Limit;
                        item.CampaignPrice = campaign.CampaignPrice;

                        break;
                    }
                }

            }
            return data;

        }
        public async Task<bool?> CreateProduct(ProductRequestModel model)
        {

            var findProduct = await _dbContext.Product.Where(x => !x.IsDeleted && x.Code==model.Code).FirstOrDefaultAsync();
            if (findProduct!=null)
            {
                return null;
            }
            var data = model.Adapt<Product>();
            await _dbContext.Product.AddAsync(data);
            var saveChanges=await _uow.CommitAsync();

            return saveChanges;
        }
    }
}
