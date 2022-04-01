using ECommerceProject.Business.IService;
using ECommerceProject.Contract.RequestModel;
using ECommerceProject.Contract.RequestModel.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetAllProduct();
            return View(productList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequestModel model)
        {
            if (model is null)
            {
                return Json(new { failed = true, message = "Please fill in the required fields" });
            }

            var createProduct = await _productService.CreateProduct(model);
            if (createProduct==true)
            {
                return Json(new { failed = false, message = "The product has been successfully registered" });
            }
            else if (createProduct==null)
            {
                return Json(new { failed = true, message = "There are products from the same product code" });
            }
            else
            {
                return Json(new { failed = true, message = "An error occurred" });
            }

         
        }
    }
}
