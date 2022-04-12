using BLL.Category;
using BLL.ProductCategory;
using BLL.ProductImage;
using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using BO.ViewModels.ProductImage;
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
                productFullVMs[i].CategoryVMs = new List<CategoryVM>();
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

            var productImageBLL = new ProductImageBLL();
            for (int i = 0; i < productFullVMs.Count; i++)
            {
                var listImg = await productImageBLL.GetByProductId(productFullVMs[i].Id);
                if (listImg.Count > 0)
                {
                    for (int j = 0; j < listImg.Count; j++)
                    {
                        productFullVMs[i].ProductImageVMs.Add(listImg[j]);
                    }
                }

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
            if (listCategoryProduct.Count > 0)
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

            var productImageBLL = new ProductImageBLL();
            var listImg = await productImageBLL.GetByProductId(productFullVM.Id);
            if (listImg.Count > 0)
            {
                productFullVM.ProductImageVMs = new List<ProductImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    productFullVM.ProductImageVMs.Add(listImg[i]);
                }
            }


            return productFullVM;
        }

        public async Task<ProductFullVM> GetBySlug(string slug)
        {
            var productFullVM = await productFullDAL.GetBySlug(slug);
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
            if (listCategoryProduct.Count >0)
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

            var productImageBLL = new ProductImageBLL();
            var listImg = await productImageBLL.GetByProductId(productFullVM.Id);
            if (listImg.Count > 0)
            {
                productFullVM.ProductImageVMs = new List<ProductImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    productFullVM.ProductImageVMs.Add(listImg[i]);
                }
            }


            return productFullVM;
        }

        public async Task<List<ProductFullVM>> GetByBrandId(string id)
        {
            var productFullVM = await productFullDAL.GetByBrandId(id);
            if (productFullVM.Count == 0)
            {
                return new List<ProductFullVM>();
            }

            var cpBLL = new ProductCategoryBLL();
            for(int i = 0; i < productFullVM.Count; i++)
            {
                #region Brand
                var brandBLL = new BrandBLL();
                var brand = await brandBLL.GetById(productFullVM[i].BrandId);
                if (brand != null)
                {
                    productFullVM[i].BrandVM = brand;
                }
                #endregion

                #region Catgeory list
                var listCategoryProduct = await cpBLL.GetById(productFullVM[i].Id, "ProductId");
                if (listCategoryProduct.Count > 0)
                {
                    productFullVM[i].CategoryVMs = new List<CategoryVM>();
                    for (int j = 0; j < listCategoryProduct.Count(); j++)
                    {
                        var categoryBLL = new CategoryBLL();
                        var productVM = await categoryBLL.GetById(listCategoryProduct[j].CategoryId);
                        if (productVM == null)
                        {
                            continue;
                        }
                        productFullVM[i].CategoryVMs.Add(productVM);
                    }
                }
                #endregion

                var productImageBLL = new ProductImageBLL();
                var listImg = await productImageBLL.GetByProductId(productFullVM[i].Id);
                if (listImg.Count > 0)
                {
                    productFullVM[i].ProductImageVMs = new List<ProductImageVM>();
                    for (int m = 0; m < listImg.Count; m++)
                    {
                        productFullVM[i].ProductImageVMs.Add(listImg[m]);
                    }
                }
            }

            


            return productFullVM;
        }

    }
}
