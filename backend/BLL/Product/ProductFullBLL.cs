using BLL.Category;
using BLL.ProductCategoryMapping;
using BOL.ViewModels.Brand;
using BOL.ViewModels.Category;
using BOL.ViewModels.Product;
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
            if (productFullVMs == null)
            {
                return null;
            }

            var cpBLL = new ProductCategoryMappingBLL();
            for (int i = 0; i < productFullVMs.Count; i++)
            {
                #region Brand
                var brandBLL = new BrandBLL();
                var brand = await brandBLL.GetById(productFullVMs[i].BrandId);
                if (brand != null)
                {
                    productFullVMs[i].BrandVM = new BrandVM();
                    productFullVMs[i].BrandVM = brand;
                }
                //else
                //{
                //    productFullVMs[i].BrandVM = new BrandVM();
                //    productFullVMs[i].BrandVM = null;
                //}
                #endregion

                #region Catgeory list
                var listCategoryProduct = await cpBLL.GetById(productFullVMs[i].Id, "ProductId");
                if (listCategoryProduct == null)
                {
                    productFullVMs[i].CategoryVMs=null;
                }
                else
                {
                    productFullVMs[i].CategoryVMs = new List<CategoryVM>();
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

            var cpBLL = new ProductCategoryMappingBLL();

            #region Brand
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(productFullVM.BrandId);
            if (brand != null)
            {
                productFullVM.BrandVM = new BrandVM();
                productFullVM.BrandVM = brand;
            }
            //else
            //{
            //    productFullVM.BrandVM = null;
            //}
            #endregion

            #region Catgeory list
            var listCategoryProduct = await cpBLL.GetById(productFullVM.Id, "ProductId");
            if (listCategoryProduct == null)
            {
                productFullVM.CategoryVMs = null;
            }
            else
            {
                productFullVM.CategoryVMs = new List<CategoryVM>();
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
