using ECommerceProject.Business.IService;
using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Contract.Common;
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
    public class ProductService : BaseService<Product>, IProductService
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
                var findCampaignList = await _dbContext.Campaign.Where(x => x.ProductId == item.Id && !x.IsDeleted && x.RecordDate.Date == DateTime.Now.Date).ToListAsync();

                foreach (var campaign in findCampaignList)
                {
                    var diff = DateTime.Now.Hour - campaign.RecordDate.Hour;
                    if (diff <= campaign.Duration)
                    {
                        item.IsActiveCampaign = true;
                        item.CampaignName = campaign.Name;
                        item.Limit = campaign.Limit;
                        item.CampaignPrice = campaign.CampaignPrice;
                        item.CampaignId = campaign.Id;

                        break;
                    }
                }

            }
            return data;

        }
        public async Task<bool?> CreateProduct(ProductRequestModel model)
        {

            var findProduct = await _dbContext.Product.Where(x => !x.IsDeleted && x.Code == model.Code).FirstOrDefaultAsync();
            if (findProduct != null)
            {
                return null;
            }
            var data = model.Adapt<Product>();
            await _dbContext.Product.AddAsync(data);
            var saveChanges = await _uow.CommitAsync();

            return saveChanges;
        }
        public async Task<ProductResponseModel> GetProductInfo(Guid Id)
        {

            var getProductInfo = await _dbContext.Product.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();
            if (getProductInfo==null)
            {
                return null;
            }

            var data = getProductInfo.Adapt<ProductResponseModel>();


            var findCampaignList = await _dbContext.Campaign.Where(x => x.ProductId ==Id && !x.IsDeleted && x.RecordDate.Date == DateTime.Now.Date).ToListAsync();

            foreach (var campaign in findCampaignList)
            {
                var diff = DateTime.Now.Hour - campaign.RecordDate.Hour;
                if (diff <= campaign.Duration)
                {
                    data.IsActiveCampaign = true;
                    data.CampaignName = campaign.Name;
                    data.Limit = campaign.Limit;
                    data.CampaignPrice = campaign.CampaignPrice;
                    data.CampaignId = campaign.Id;

                    break;
                }
            }


            return data;

        }
        public async Task<BaseResponseModel> DeleteProduct(ByIdRequestModel model)
        {
            var result = new BaseResponseModel();

            var findProduct = await _dbContext.Product.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefaultAsync();
            if (findProduct == null)
            {
                result.IsError = true;
                result.Detail = "No product found to be deleted";
                return result;
            }

            findProduct.IsDeleted = true;
            var saveChanges = await _uow.CommitAsync();

            if (saveChanges)
            {
                result.Detail = "Record successfully deleted";
            }
            else
            {
                result.IsError = true;
                result.Detail = "An error occurred";
            }

            return result;
        }
    }
}
