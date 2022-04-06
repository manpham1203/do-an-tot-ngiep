using BLL.ProductCategory;
using DAL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BO.ViewModels.Product;
using BO.ViewModels.ProductCategory;
using BLL.Category;
using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using System.IO;
using System.Threading;

namespace BLL.Product
{
    public class ProductBLL
    {
        private readonly ProductDAL productDAL;
        private Common cm;
        public ProductBLL()
        {
            productDAL = new ProductDAL();
        }
        public async Task<List<ProductVM>> GetAll()
        {
            return await productDAL.GetAll();
        }
        public async Task<ProductVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            return await productDAL.GetById(id);
        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            return await productDAL.GetByBrandId(id);
        }
        public async Task<bool> Create(CreateProductVM createProductVM)
        {
            var brandBLL = new BrandBLL();
            var brandId = await brandBLL.GetById(createProductVM.BrandId);
            if (brandId == null)
            {
                return false;
            }

            if (createProductVM.CategoryIds.Count > 0)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < createProductVM.CategoryIds.Count; i++)
                {
                    var categories = await categoryBLL.GetById(createProductVM.CategoryIds[i]);
                    if (categories == null)
                    {
                        return false;
                    }
                }
            }

            cm = new Common();
            var productId = cm.RandomString(12);
            var checkIdExists = await GetById(productId);
            while (checkIdExists != null)
            {
                productId = cm.RandomString(12);
                checkIdExists = await GetById(productId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(createProductVM.Name).Trim().ToLower(), @"\s+", "-");


            if (createProductVM.Files.Count > 0)
            {
                createProductVM.ImageNames = new List<string>();
                for (int i = 0; i < createProductVM.Files.Count; i++)
                {
                    string imageName = slug;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(createProductVM.Files[i].FileName);
                    createProductVM.ImageNames.Add(imageName);
                    Thread.Sleep(200);
                }
            }

            #region Product
            var productVM = new ProductVM
            {
                Id = productId,
                Name = createProductVM.Name,
                Slug = slug,
                Price = createProductVM.Price,
                PriceDiscount = createProductVM.PriceDiscount,
                FullDescription = createProductVM.FullDescription,
                ShortDescription = createProductVM.ShortDescription,
                Quantity = createProductVM.Quantity,
                Pulished = createProductVM.Pulished,
                Deleted = false,
                Likes = 0,
                Views = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                BrandId = createProductVM.BrandId,
            };

            #endregion
            var productCreate = await productDAL.Create(productVM);
            if (!productCreate)
            {
                return false;
            }

            #region Product_Category

            if (createProductVM.CategoryIds.Count > 0)
            {
                for (int i = 0; i < createProductVM.CategoryIds.Count; i++)
                {
                    var pcmVM = new ProductCategoryVM
                    {
                        CategoryId = createProductVM.CategoryIds[i],
                        ProductId = productId
                    };
                    var pcmBLL = new ProductCategoryBLL();
                    var pcmCreate = await pcmBLL.Create(pcmVM);
                    if (pcmCreate == false)
                    {
                        return false;
                    }
                }
            }

            #endregion    
            
            //if (createProductVM.Files.Count > 0)
            //{
            //    var productImageBLL = new ProductImageBLL();
            //    var saveImg = await brandImageBLL.Create(model.ImageNames, brandId);
            //    if (!saveImg)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }
        public async Task<bool> Update(string id, UpdateProductVM updateProductVM)
        {
            var brandBLL = new BrandBLL();
            var brandId = await brandBLL.GetById(updateProductVM.BrandId);
            if (brandId == null)
            {
                return false;
            }
            if (updateProductVM.CategoryIds != null)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
                {
                    var categories = await categoryBLL.GetById(updateProductVM.CategoryIds[i]);
                    if (categories == null)
                    {
                        return false;
                    }
                }
            }


            cm = new Common();
            var checkProduct = await GetById(id);
            if (checkProduct == null)
            {
                return false;
            }

            var slug = Regex.Replace(cm.RemoveUnicode(updateProductVM.Name).Trim().ToLower(), @"\s+", "-");

            #region Product
            var productVM = new ProductVM
            {
                Id = id,
                Name = updateProductVM.Name,
                Slug = slug,
                Price = updateProductVM.Price,
                PriceDiscount = updateProductVM.PriceDiscount,
                FullDescription = updateProductVM.FullDescription,
                ShortDescription = updateProductVM.ShortDescription,
                Quantity = updateProductVM.Quantity,
                Pulished = updateProductVM.Pulished,
                Deleted = updateProductVM.Deleted,
                Likes = updateProductVM.Likes,
                Views = updateProductVM.Views,
                UpdatedAt = DateTime.Now,
                BrandId = updateProductVM.BrandId,
            };

            #endregion

            #region Product_Category
            var pcmBLL = new ProductCategoryBLL();
            var listProCatMapping = await pcmBLL.GetById(id, "ProductId");
            if (listProCatMapping.Count > 0)
            {
                var delete = await pcmBLL.Delete(id, "ProductId");

                if (delete)
                {
                    if (updateProductVM.CategoryIds.Count > 0)
                    {
                        for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
                        {
                            var pcmVM = new ProductCategoryVM
                            {
                                CategoryId = updateProductVM.CategoryIds[i],
                                ProductId = id
                            };
                            var pcmCreate = await pcmBLL.Create(pcmVM);
                            if (pcmCreate == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                updateProductVM.CategoryIds = null;
            }
            if (updateProductVM.CategoryIds.Count >0)
            {
                for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
                {
                    var pcmVM = new ProductCategoryVM
                    {
                        CategoryId = updateProductVM.CategoryIds[i],
                        ProductId = id
                    };
                    var pcmCreate = await pcmBLL.Create(pcmVM);
                    if (pcmCreate == false)
                    {
                        return false;
                    }
                }
            }
            #endregion
            return await productDAL.Update(productVM);
        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var productFullBLL = new ProductFullBLL();
            var productFullVM = await productFullBLL.GetById(id);
            if (productFullVM == null)
            {
                return false;
            }

            if (productFullVM.CategoryVMs != null)
            {
                var pcmBLL = new ProductCategoryBLL();
                var listProCatMapping = await pcmBLL.GetById(id, "ProductId");
                if (listProCatMapping != null)
                {
                    var delete = await pcmBLL.Delete(id, "ProductId");
                    if (delete == false)
                    {
                        return false;
                    }
                }

            }


            return await productDAL.Delete(id);
        }

        public async Task<List<ProductFullVM>> GetProductFullAll()
        {
            var productFullVMs = await productDAL.GetProductFullAll();
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
                    productFullVMs[i].CategoryVMs = null;
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
        public async Task<ProductFullVM> GetProductFullById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var productFullVM = await productDAL.GetProductFullById(id);
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
