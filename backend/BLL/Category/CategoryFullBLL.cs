using BLL.Picture;
using BLL.Product;
using BLL.ProductCategory;
using BO.ViewModels.Category;
using BO.ViewModels.Picture;
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
        private string objectType = "category";
        public CategoryFullBLL()
        {
            categoryFullDAL = new CategoryFullDAL();
        }
        public async Task<List<CategoryFullVM>> GetAll()
        {
            var categoryFullVMs = await categoryFullDAL.GetAll();
            if (categoryFullVMs.Count == 0)
            {
                return categoryFullVMs;
            }
            var cpBLL = new ProductCategoryBLL();
            for (int i = 0; i < categoryFullVMs.Count; i++)
            {

                var listCategoryProduct = await cpBLL.GetById(categoryFullVMs[i].Id, "CategoryId");
                if (listCategoryProduct.Count > 0)
                {
                    for (int j = 0; j < listCategoryProduct.Count(); j++)
                    {
                        var productBLL = new ProductBLL();
                        var productVM = await productBLL.GetById(listCategoryProduct[j].ProductId);
                        categoryFullVMs[i].ProductVMs.Add(productVM);
                    }
                }
            }

            var categoryImageBLL = new PictureBLL();
            for (int i = 0; i < categoryFullVMs.Count; i++)
            {
                var listImg = await categoryImageBLL.GetByObjectId(categoryFullVMs[i].Id, objectType);
                if (listImg[0] != null)
                {
                    categoryFullVMs[i].PictureVM = listImg[0];
                }

            }


            return categoryFullVMs;
        }
        public async Task<CategoryFullVM> GetById(string id)
        {
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

            var categoryImageBLL = new PictureBLL();
            var listImg = await categoryImageBLL.GetByObjectId(categoryFullVM.Id, objectType);
            if (listImg[0] != null)
            {
                categoryFullVM.PictureVM = listImg[0];
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
            var categoryImageBLL = new PictureBLL();
            var listImg = await categoryImageBLL.GetByObjectId(categoryFullVM.Id, objectType);
            if (listImg[0] != null)
            {
                categoryFullVM.PictureVM = listImg[0];
            }


            return categoryFullVM;
        }
    }
}
