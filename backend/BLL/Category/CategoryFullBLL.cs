using BLL.CategoryImage;
using BLL.Product;
using BLL.ProductCategory;
using BO.ViewModels.Category;
using BO.ViewModels.CategoryImage;
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
                if (listCategoryProduct.Count >0)
                {
                    for (int j = 0; j < listCategoryProduct.Count(); j++)
                    {
                        var productBLL = new ProductBLL();
                        var productVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                        categoryFullVMs[i].ProductVMs.Add(productVM);
                    }
                }
            }

            var categoryImageBLL = new CategoryImageBLL();
            for (int i = 0; i < categoryFullVMs.Count; i++)
            {
                var listImg = await categoryImageBLL.GetByCategoryId(categoryFullVMs[i].Id);
                if (listImg.Count > 0)
                {
                    categoryFullVMs[i].CategoryImageVMs = new List<CategoryImageVM>();
                    for (int j = 0; j < listImg.Count; j++)
                    {
                        categoryFullVMs[i].CategoryImageVMs.Add(listImg[j]);
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
            if (listCategoryProduct.Count > 0)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var productBLL = new ProductBLL();
                    var categoryVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                    categoryFullVM.ProductVMs.Add(categoryVM);
                }
            }

            var categoryImageBLL = new CategoryImageBLL();
            var listImg = await categoryImageBLL.GetByCategoryId(categoryFullVM.Id);
            if (listImg.Count > 0)
            {
                categoryFullVM.CategoryImageVMs = new List<CategoryImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    categoryFullVM.CategoryImageVMs.Add(listImg[i]);
                }
            }

            return categoryFullVM;
        }
        public async Task<CategoryFullVM> GetBySlug(string slug)
        {
            var categoryFullVM = await categoryFullDAL.GetBySlug(slug);
            if (categoryFullVM == null)
            {
                return null;
            }
            var cpBLL = new ProductCategoryBLL();

            var listCategoryProduct = await cpBLL.GetById(categoryFullVM.Id, "CategoryId");
            if (listCategoryProduct.Count > 0)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var productBLL = new ProductBLL();
                    var categoryVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                    categoryFullVM.ProductVMs.Add(categoryVM);
                }
            }
            var categoryImageBLL = new CategoryImageBLL();
            var listImg = await categoryImageBLL.GetByCategoryId(categoryFullVM.Id);
            if (listImg.Count > 0)
            {
                categoryFullVM.CategoryImageVMs = new List<CategoryImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    categoryFullVM.CategoryImageVMs.Add(listImg[i]);
                }
            }


            return categoryFullVM;
        }
    }
}
