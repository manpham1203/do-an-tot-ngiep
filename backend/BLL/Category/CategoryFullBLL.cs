using BLL.Product;
using BLL.ProductCategory;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using DAL.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Category
{
    public class CategoryFullBLL
    {
        private readonly CategoryFullDAL categoryFullDAL;
        public CategoryFullBLL()
        {
            categoryFullDAL = new CategoryFullDAL();
        }
        public async Task<List<CategoryFullVM>> GetAll()
        {
            var categoryFullVMs = await categoryFullDAL.GetAll();
            if (categoryFullVMs.Count==0)
            {
                return categoryFullVMs;
            }
            var cpBLL = new ProductCategoryBLL();
            for (int i = 0; i < categoryFullVMs.Count; i++)
            {

                var listCategoryProduct = await cpBLL.GetById(categoryFullVMs[i].Id, "CategoryId");
                if (listCategoryProduct == null)
                {
                    categoryFullVMs[i].ProductVMs = null;
                }
                else
                {
                    categoryFullVMs[i].ProductVMs = new List<ProductVM>();
                    for (int j = 0; j < listCategoryProduct.Count(); j++)
                    {
                        var productBLL = new ProductBLL();
                        var productVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                        categoryFullVMs[i].ProductVMs.Add(productVM);
                    }
                }

            }


            return categoryFullVMs;
        }
        public async Task<CategoryFullVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var categoryFullVM = await categoryFullDAL.GetById(id);
            if (categoryFullVM == null)
            {
                return null;
            }
            var cpBLL = new ProductCategoryBLL();


            var listCategoryProduct = await cpBLL.GetById(categoryFullVM.Id, "CategoryId");
            if (listCategoryProduct == null)
            {
                categoryFullVM.ProductVMs= null;
            }
            else
            {
                categoryFullVM.ProductVMs = new List<ProductVM>();
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var productBLL = new ProductBLL();
                    var categoryVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                    categoryFullVM.ProductVMs.Add(categoryVM);
                }
            }

            return categoryFullVM;
        }

    }
}
