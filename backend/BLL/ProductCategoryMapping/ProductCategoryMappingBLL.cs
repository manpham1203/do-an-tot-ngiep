using BLL.Category;
using BO.ViewModels.ProductCategoryMapping;
using DAL.ProductCategoryMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductCategoryMapping
{
    public class ProductCategoryMappingBLL
    {
        private readonly ProductCategoryMappingDAL pcmDAL;
        public ProductCategoryMappingBLL()
        {
            pcmDAL = new ProductCategoryMappingDAL();
        }
        public async Task<List<ProductCategoryMappingVM>> GetAll()
        {
            var result = await pcmDAL.GetAll();
            if (result == null)
            {
                return null;
            }
            return result;
        }
        public async Task<List<ProductCategoryMappingVM>> GetById(string id, string type)
        {
            var result = new List<ProductCategoryMappingVM>();
            switch (type)
            {
                case "ProductId":
                    result = await pcmDAL.GetById(id, type="ProductId");
                    break;
                case "CategoryId":
                    result = await pcmDAL.GetById(id, type = "CategoryId");
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<bool> Create(ProductCategoryMappingVM model)
        {
            var categoryBLL = new CategoryBLL();
            var category = await categoryBLL.GetById(model.CategoryId);
            if (category == null)
            {
                return false;
            }
            var result = await pcmDAL.Create(model);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id, string type)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var result = false;
            switch (type)
            {
                case "ProductId":
                    var productId = await pcmDAL.GetById(id, type);
                    if (productId.Count==0)
                    {
                        return true;
                    }
                    else
                    {
                        result = await pcmDAL.Delete(id, type);
                    }
                    break;
                case "CategoryId":
                    var categoryId = await pcmDAL.GetById(id, type);
                    if (categoryId.Count==0)
                    {
                        return true;
                    }
                    else
                    {
                        result = await pcmDAL.Delete(id, type);
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
