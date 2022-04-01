using ECommerceProject.Contract.Common;
using ECommerceProject.Contract.RequestModel.Campaign;
using ECommerceProject.Contract.ResponseModel.Campaign;
using ECommerceProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.IService
{
    public interface ICampaignService : IBaseService<Campaign>
    {
        Task<List<CampaignResponseModel>> GetAllCampaign();
        Task<BaseResponseModel> CreateCampaign(CampaignRequestModel model);
        Task<BaseResponseModel> DeleteCampaign(ByIdRequestModel model);
    }
}
