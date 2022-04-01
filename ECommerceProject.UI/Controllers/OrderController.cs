using ECommerceProject.Business.IService;
using ECommerceProject.Contract.RequestModel.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public OrderController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetAllProduct();
            return View(productList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestModel model)
        {
            if (model is null)
            {
                return Json(new { failed = true, message = "Please fill in the required fields" });
            }

            var createOrder = await _orderService.CreateOrder(model);
            if (createOrder.IsError == false)
            {
                return Json(new { failed = false, message = createOrder.Detail });
            }
            else
            {
                return Json(new { failed = true, message = createOrder.Detail });
            }


        }

        public async Task<IActionResult> GetProductInfo(Guid Id)
        {

            var getProductInfo = await _productService.GetProductInfo(Id);

            return View(getProductInfo);

        }

        public async Task<IActionResult> ShoppingList()
        {
            var orderList = await _orderService.GetAllOrder();
            return View(orderList);
        }
    }
}
