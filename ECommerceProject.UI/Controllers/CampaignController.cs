using ECommerceProject.Business.IService;
using ECommerceProject.Contract.RequestModel.Campaign;
using ECommerceProject.Contract.ResponseModel.Campaign;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.UI.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService= campaignService;
        }
        public async Task<IActionResult> Index()
        {
            var campaignList = await _campaignService.GetAllCampaign();
            return View(campaignList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CampaignRequestModel model)
        {
            if (model is null)
            {
                return Json(new { failed = true, message = "Please fill in the required fields" });
            }

            var createCampaign = await _campaignService.CreateCampaign(model);
            if (createCampaign.IsError == false)
            {
                return Json(new { failed = false, message = createCampaign.Detail });
            }
            else
            {
                return Json(new { failed = true, message =createCampaign.Detail });
            }


        }
    }
}
