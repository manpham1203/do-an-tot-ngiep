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
using BLL.ProductImage;

namespace BLL.Product
{
    public class ProductBLL
    {
        private ProductDAL productDAL;
        private CommonBLL cm;
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

            cm = new CommonBLL();
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
                Published = createProductVM.Published,
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

            if (createProductVM.Files.Count > 0)
            {
                var productImageBLL = new ProductImageBLL();
                var saveImg = await productImageBLL.Create(createProductVM.ImageNames, productId);
                if (!saveImg)
                {
                    return false;
                }
            }

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


            cm = new CommonBLL();
            var checkProduct = await GetById(id);
            if (checkProduct == null)
            {
                return false;
            }

            var slug = Regex.Replace(cm.RemoveUnicode(updateProductVM.Name).Trim().ToLower(), @"\s+", "-");


            if (updateProductVM.Files.Count > 0)
            {
                updateProductVM.ImageNames = new List<string>();
                for (int i = 0; i < updateProductVM.Files.Count; i++)
                {
                    string imageName = slug;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(updateProductVM.Files[i].FileName);
                    updateProductVM.ImageNames.Add(imageName);
                    Thread.Sleep(200);
                }
            }

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
                Published = updateProductVM.Published,
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
            updateProductVM.CategoryIds = new List<string>();
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
            #endregion

            var saveProduct = await productDAL.Update(productVM);
            if (saveProduct == false)
            {
                return false;
            }

            if (updateProductVM.Files.Count > 0)
            {
                var productImageBLL = new ProductImageBLL();
                var saveImg = await productImageBLL.Create(updateProductVM.ImageNames, id);
                if (!saveImg)
                {
                    return false;
                }
            }

            return true;
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
        public async Task<bool> Published(string id)
        {
            var productVM = await GetById(id);
            if (productVM == null)
            {
                return false;
            }
            bool published = !productVM.Published;
            var result = await productDAL.Pulished(id, published);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id)
        {
            var brandVM = await GetById(id);
            if (brandVM == null)
            {
                return false;
            }
            bool deleted = !brandVM.Deleted;
            var result = await productDAL.Deleted(id, deleted);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<List<ProductVM>> GetAllProductDeleted(bool deleted)
        {
            return await productDAL.GetAllProductDeleted(deleted);
        }

        public async Task<List<ProductCardVM>> ListProductCard()
        {
            var resultFromDAL = await productDAL.ListProductCard();
            if (resultFromDAL == null)
            {
                return null;
            }
            for (int i = 0; i < resultFromDAL.Count; i++)
            {
                var brandBLL = new BrandBLL();
                var brand = await brandBLL.GetById(resultFromDAL[i].BrandId);
                if (brand != null)
                {
                    resultFromDAL[i].BrandNameVM.Id = brand.Id;
                    resultFromDAL[i].BrandNameVM.Name = brand.Name;
                    resultFromDAL[i].BrandNameVM.Slug = brand.Slug;
                }

                var productImageBLL = new ProductImageBLL();
                var listImg = await productImageBLL.GetByProductId(resultFromDAL[i].Id);
                if (listImg.Count > 0)
                {
                    resultFromDAL[i].ImageName = listImg[0].Name;
                }
            }
            return resultFromDAL;
        }

        public async Task<ProductCardVM> ProductCardById(string id)
        {
            var resultFromDAL = await productDAL.ProductCardById(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(resultFromDAL.BrandId);
            if (brand != null)
            {
                resultFromDAL.BrandNameVM.Id = brand.Id;
                resultFromDAL.BrandNameVM.Name = brand.Name;
                resultFromDAL.BrandNameVM.Slug = brand.Slug;
            }

            var productImageBLL = new ProductImageBLL();
            var listImg = await productImageBLL.GetByProductId(resultFromDAL.Id);
            if (listImg.Count > 0)
            {
                resultFromDAL.ImageName = listImg[0].Name;
            }
            return resultFromDAL;
        }

        public async Task<List<ProductCardVM>> ListProductCardOfBrand(string id)
        {
            var resultFromDAL = await productDAL.ListProductCardOfBrand(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<ProductCardVM>();
            }
            for (int i = 0; i < resultFromDAL.Count; i++)
            {
                var brandBLL = new BrandBLL();
                var brand = await brandBLL.GetById(resultFromDAL[i].BrandId);
                if (brand != null)
                {
                    resultFromDAL[i].BrandNameVM.Id = brand.Id;
                    resultFromDAL[i].BrandNameVM.Name = brand.Name;
                    resultFromDAL[i].BrandNameVM.Slug = brand.Slug;
                }

                var productImageBLL = new ProductImageBLL();
                var listImg = await productImageBLL.GetByProductId(resultFromDAL[i].Id);
                if (listImg.Count > 0)
                {
                    resultFromDAL[i].ImageName = listImg[0].Name;
                }
            }
            return resultFromDAL;
        }

        public async Task<ProductRowAdminVM> ProductRowAdmin(string id)
        {
            var resultFromDAL = await productDAL.ProductRowAdmin(id);
            if (resultFromDAL == null)
            {
                return new ProductRowAdminVM();
            }
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(resultFromDAL.BrandId);
            if (brand != null)
            {
                resultFromDAL.BrandNameVM.Id = brand.Id;
                resultFromDAL.BrandNameVM.Name = brand.Name;
                resultFromDAL.BrandNameVM.Slug = brand.Slug;
            }

            var pcBLL = new ProductCategoryBLL();
            var listCategoryProduct = await pcBLL.GetById(resultFromDAL.Id, "ProductId");
            if (listCategoryProduct.Count > 0)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var categoryBLL = new CategoryBLL();
                    var category = await categoryBLL.CategoryNameById(listCategoryProduct[j].CategoryId);
                    if (category == null)
                    {
                        continue;
                    }
                    resultFromDAL.CategoryNameVMs.Add(category);
                }
            }


            var productImageBLL = new ProductImageBLL();
            var listImg = await productImageBLL.GetByProductId(resultFromDAL.Id);
            if (listImg.Count > 0)
            {
                resultFromDAL.ImageName = listImg[0].Name;
            }
            return resultFromDAL;
        }

        public async Task<List<ProductNameVM>> AllProductName()
        {
            var resultFromDAL = await productDAL.AllProductName();
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<ProductNameVM>();
            }
            return resultFromDAL;
        }
        public async Task<List<ProductNameVM>> AllProductName(bool deleted)
        {
            var resultFromDAL = await productDAL.AllProductName(deleted);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<ProductNameVM>();
            }
            return resultFromDAL;
        }
        public async Task<ProductPaginationAdminVM> AllProductNameAdmin(bool deleted, ProductFilterVM model)
        {
            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Search.ToLower();
            }
            var resultFromDAL = await productDAL.AllProductNameAdmin(deleted, model);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new ProductPaginationAdminVM
                {
                    TotalPage = 0,
                    TotalResult = 0,
                    Products = new List<ProductNameVM>(),
                };
            }

            var tempBrand = new ProductPaginationAdminVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductNameVM>(),
            };

            if (model.BrandIds.Count > 0)
            {

                for (int i = 0; i < model.BrandIds.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var checkBrand = await GetById(resultFromDAL[j].Id);
                        if (checkBrand != null)
                        {
                            if (model.BrandIds[i] == checkBrand.BrandId)
                            {
                                tempBrand.Products.Add(resultFromDAL[j]);
                            }
                        }
                    }
                }
            }

            var tempCategory = new ProductPaginationAdminVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductNameVM>(),
            };
            var pcBLL = new ProductCategoryBLL();
            if (model.CategoryIds.Count > 0)
            {
                for (int i = 0; i < model.CategoryIds.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var checkCategory = await pcBLL.GetById(model.CategoryIds[i], "CategoryId");
                        if (checkCategory.Count > 0)
                        {
                            for (int c = 0; c < checkCategory.Count; c++)
                            {
                                if (resultFromDAL[j].Id == checkCategory[c].ProductId)
                                {
                                    tempCategory.Products.Add(resultFromDAL[j]);
                                }
                            }
                        }
                    }
                }
                for(int i = 0; i < tempCategory.Products.Count; i++)
                {
                    for(int j = 0; j < tempCategory.Products.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        if (tempCategory.Products[i].Id == tempCategory.Products[j].Id)
                        {
                            tempCategory.Products.Remove(tempCategory.Products[j]);
                        }
                    }
                }
            }

            var tempFinal = new ProductPaginationAdminVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductNameVM>(),
            };

            if (model.BrandIds.Count > 0 && model.CategoryIds.Count > 0)
            {
                var tempProduct = new List<ProductNameVM>();
                for (int j = 0; j < tempCategory.Products.Count; j++)
                {
                    for (int m = 0; m < model.BrandIds.Count; m++)
                    {
                        if (tempCategory.Products[j].BrandId == model.BrandIds[m])
                        {
                            tempProduct.Add(tempCategory.Products[j]);
                        }
                    }
                }
                tempFinal.Products = tempProduct;
            }

            if (model.BrandIds.Count > 0 && model.CategoryIds.Count > 0)
            {
                var countTemp = tempFinal.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempFinal.Products = tempFinal.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempFinal.TotalPage = totalPageTemp;
                tempFinal.TotalResult = countTemp;
                return tempFinal;
            }


            if (model.BrandIds.Count > 0 && model.CategoryIds.Count == 0)
            {
                var countTemp = tempBrand.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempBrand.Products = tempBrand.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempBrand.TotalPage = totalPageTemp;
                tempBrand.TotalResult = countTemp;

                return tempBrand;
            }

            if (model.CategoryIds.Count > 0 && model.BrandIds.Count == 0)
            {
                var countTemp = tempCategory.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempCategory.Products = tempCategory.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempCategory.TotalPage = totalPageTemp;
                tempCategory.TotalResult = countTemp;

                return tempCategory;
            }

            var count = resultFromDAL.Count();
            var totalPage = (int)Math.Ceiling(count / (double)model.Limit);
            resultFromDAL = resultFromDAL.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();

            var result = new ProductPaginationAdminVM
            {
                TotalPage = totalPage,
                Products = resultFromDAL,
                TotalResult = count,
            };

            return result;
        }

        public async Task<PriceRangeVM> PriceRange()
        {
            return await productDAL.PriceRange();
        }
        public async Task<ProductDetailVM> ProductDetail(string slug)
        {
            var resultFromDAL = await productDAL.ProductDetail(slug);
            if (resultFromDAL == null)
            {
                return null;
            }
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(resultFromDAL.BrandId);
            if (brand != null)
            {
                resultFromDAL.BrandNameVM.Id = brand.Id;
                resultFromDAL.BrandNameVM.Name = brand.Name;
                resultFromDAL.BrandNameVM.Slug = brand.Slug;
            }

            var pcBLL = new ProductCategoryBLL();
            var listCategoryProduct = await pcBLL.GetById(resultFromDAL.Id, "ProductId");
            if (listCategoryProduct.Count > 0)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var categoryBLL = new CategoryBLL();
                    var category = await categoryBLL.CategoryNameById(listCategoryProduct[j].CategoryId);
                    if (category == null)
                    {
                        continue;
                    }
                    resultFromDAL.CategoryNameVMs.Add(category);
                }
            }

            var productImageBLL = new ProductImageBLL();
            var listImg = await productImageBLL.GetByProductId(resultFromDAL.Id);
            resultFromDAL.ProductImageVMs = listImg;

            return resultFromDAL;
        }

        public async Task<bool> CheckExists(string id)
        {
            return await productDAL.CheckExists(id);
        }
        public async Task<CartRowVM> CartRow(string id)
        {
            var resultFromDb = await productDAL.CartRow(id);
            if (resultFromDb == null)
            {
                return null;
            }
            return resultFromDb;
        }
        public async Task<List<CartRowVM>> CartRows(List<string> ids)
        {
            var listProduct = new List<CartRowVM>();
            for (int i = 0; i < ids.Count; i++)
            {
                var check = await productDAL.CheckExists(ids[i]);
                if (check)
                {
                    var resultFromDb = await CartRow(ids[i]);
                    if (resultFromDb != null)
                    {
                        var brandBLL = new BrandBLL();
                        var brand = await brandBLL.GetById(resultFromDb.BrandId);
                        resultFromDb.BrandNameVM = new BrandNameVM();
                        if (brand != null)
                        {
                            resultFromDb.BrandNameVM.Id = brand.Id;
                            resultFromDb.BrandNameVM.Name = brand.Name;
                            resultFromDb.BrandNameVM.Slug = brand.Slug;
                        }

                        var productImageBLL = new ProductImageBLL();
                        var listImg = await productImageBLL.GetByProductId(resultFromDb.Id);
                        if (listImg.Count > 0)
                        {
                            resultFromDb.ImageName = listImg[0].Name;
                        }
                        listProduct.Add(resultFromDb);
                    }
                }


            }

            return listProduct;
        }

    }
}
