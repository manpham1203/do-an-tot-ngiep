using BLL.Category;
using BLL.ProductCategory;
using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using DAL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class ProductFullBLL
    {
        private readonly ProductFullDAL productFullDAL;
        public ProductFullBLL()
        {
            productFullDAL = new ProductFullDAL();
        }
        public async Task<List<ProductFullVM>> GetAll()
        {
            var productFullVMs = await productFullDAL.GetAll();
            if (productFullVMs.Count == 0)
            {
                return productFullVMs;
            }

            var cpBLL = new ProductCategoryBLL();
            for (int i = 0; i < productFullVMs.Count; i++)
            {
                #region Brand
                var brandBLL = new BrandBLL();
                var brand = await brandBLL.GetById(productFullVMs[i].BrandId);
                if (brand != null)
                {
                    productFullVMs[i].BrandVM = brand;
                }
                #endregion

                #region Catgeory list
                var listCategoryProduct = await cpBLL.GetById(productFullVMs[i].Id, "ProductId");
                if (listCategoryProduct.Count > 0)
                {
                    for (int j = 0; j < listCategoryProduct.Count(); j++)
                    {
                        var categoryBLL = new CategoryBLL();
                        var productVM = await categoryBLL.GetById(listCategoryProduct[j].CategoryId);
                        if (productVM == null)
                        {
                            continue;
                        }
                        productFullVMs[i].CategoryVMs.Add(productVM);
                    }
                }
                #endregion
            }

            return productFullVMs;
        }
        public async Task<ProductFullVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var productFullVM = await productFullDAL.GetById(id);
            if (productFullVM == null)
            {
                return null;
            }

            var cpBLL = new ProductCategoryBLL();

            #region Brand
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(productFullVM.BrandId);
            if (brand != null)
            {
                productFullVM.BrandVM = brand;
            }
            #endregion

            #region Catgeory list
            var listCategoryProduct = await cpBLL.GetById(productFullVM.Id, "ProductId");
            if (listCategoryProduct == null)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var categoryBLL = new CategoryBLL();
                    var productVM = await categoryBLL.GetById(listCategoryProduct[j].CategoryId);
                    if (productVM == null)
                    {
                        continue;
                    }
                    productFullVM.CategoryVMs.Add(productVM);
                }
            }
            #endregion


            return productFullVM;
        }
    }
}
