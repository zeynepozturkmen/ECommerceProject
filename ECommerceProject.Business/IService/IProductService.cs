using ECommerceProject.Contract.Common;
using ECommerceProject.Contract.RequestModel.Product;
using ECommerceProject.Contract.ResponseModel.Product;
using ECommerceProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.IService
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<ProductResponseModel>> GetAllProduct();
        Task<bool?> CreateProduct(ProductRequestModel model);
        Task<ProductResponseModel> GetProductInfo(Guid Id);
        Task<BaseResponseModel> DeleteProduct(ByIdRequestModel model);
    }
}
