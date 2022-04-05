using BLL.Category;
using BO.ViewModels.ProductCategory;
using DAL.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductCategory
{
    public class ProductCategoryBLL
    {
        private readonly ProductCategoryDAL pcmDAL;
        public ProductCategoryBLL()
        {
            pcmDAL = new ProductCategoryDAL();
        }
        public async Task<List<ProductCategoryVM>> GetAll()
        {
            return await pcmDAL.GetAll();
        }
        public async Task<List<ProductCategoryVM>> GetById(string id, string type)
        {
            var result = new List<ProductCategoryVM>();
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

        public async Task<bool> Create(ProductCategoryVM model)
        {
            return await pcmDAL.Create(model);
        }
        public async Task<bool> Delete(string id, string type)
        {
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
