using ECommerceProject.Business.IService;
using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Contract.Common;
using ECommerceProject.Contract.RequestModel.Campaign;
using ECommerceProject.Contract.ResponseModel.Campaign;
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
    public class CampaignService : BaseService<Campaign>, ICampaignService
    {
        public CampaignService(ECommerceDbContext dbContext, IUnitOfWork uow) : base(dbContext, uow)
        {

        }
        public async Task<List<CampaignResponseModel>> GetAllCampaign()
        {

            var campaignList = await _dbContext.Campaign.Where(x => !x.IsDeleted).Include(x=>x.Product).ToListAsync();

            var resCampaignList = campaignList.Adapt<List<CampaignResponseModel>>();

            foreach (var item in resCampaignList)
            {
                if (DateTime.Now.Date==item.RecordDate.Date)
                {
                    var diff = DateTime.Now.Hour - item.RecordDate.Hour;
                    if (diff<=item.Duration)
                    {
                        item.IsActive = true;
                    }
                }
            }

   
            return resCampaignList;

        }
        public async Task<BaseResponseModel> CreateCampaign(CampaignRequestModel model)
        {
            var result = new BaseResponseModel();

            var findProduct = await _dbContext.Product.Where(x => !x.IsDeleted && x.Code == model.ProductCode).FirstOrDefaultAsync();
            if (findProduct == null)
            {
                result.IsError = true;
                result.Detail = "No products found to be added to the campaign";
                return result;
            }

            var campaign = model.Adapt<Campaign>();
            campaign.ProductId = findProduct.Id;
            await _dbContext.Campaign.AddAsync(campaign);
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

        public async Task<BaseResponseModel> DeleteCampaign(ByIdRequestModel model)
        {
            var result = new BaseResponseModel();

            var findCampaign= await _dbContext.Campaign.Where(x => !x.IsDeleted && x.Id==model.Id).FirstOrDefaultAsync();
            if (findCampaign == null)
            {
                result.IsError = true;
                result.Detail = "No campaign found to be deleted";
                return result;
            }

            findCampaign.IsDeleted = true;
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