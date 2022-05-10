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
using BLL.Picture;
using BO.ViewModels.Picture;
using BLL.Comment;
using BLL.Wishlist;

namespace BLL.Product
{
    public class ProductBLL
    {
        private ProductDAL productDAL;
        private CommonBLL cm;
        private string objectType = "product";
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
            return await productDAL.GetById(id);
        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {
            return await productDAL.GetByBrandId(id);
        }
        public async Task<bool> Create(CreateProductVM createProductVM)
        {
            var brandBLL = new BrandBLL();
            var brandId = await brandBLL.CheckExistsId(createProductVM.BrandId);
            if (brandId == false)
            {
                return false;
            }

            if (createProductVM.CategoryIds.Count > 0)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < createProductVM.CategoryIds.Count; i++)
                {
                    var categories = await categoryBLL.CheckExistsId(createProductVM.CategoryIds[i]);
                    if (categories == false)
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


            if (createProductVM.Files != null)
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
                QuantityInStock = createProductVM.QuantityInStock,
                Published = createProductVM.Published,
                Deleted = false,
                Like = 0,
                View = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                BrandId = createProductVM.BrandId,
            };

            #endregion


            #region Product_Category
            var listPC = new List<ProductCategoryVM>();
            if (createProductVM.CategoryIds.Count > 0)
            {
                for (int i = 0; i < createProductVM.CategoryIds.Count; i++)
                {
                    var pcmVM = new ProductCategoryVM
                    {
                        CategoryId = createProductVM.CategoryIds[i],
                        ProductId = productId
                    };
                    listPC.Add(pcmVM);
                }
            }

            #endregion

            var pictures = new List<PictureVM>();
            if (createProductVM.Files != null)
            {
                var pictureBLL = new PictureBLL();
                for (int i = 0; i < createProductVM.Files.Count; i++)
                {
                    var pictureId = cm.RandomString(16);
                    var checkPictureId = await pictureBLL.CheckExists(pictureId);
                    while (checkPictureId)
                    {
                        pictureId = cm.RandomString(16);
                        checkPictureId = await pictureBLL.CheckExists(pictureId);
                    }
                    var pictureVM = new PictureVM
                    {
                        Id = pictureId,
                        Name = createProductVM.ImageNames[i],
                        ObjectId = productId,
                        ObjectType = objectType,
                        Published = true,
                    };
                    pictures.Add(pictureVM);
                }

            }

            var productCreate = await productDAL.Create(productVM, pictures, listPC);
            if (!productCreate)
            {
                return false;
            }


            return true;
        }
        public async Task<bool> Update(string id, UpdateProductVM updateProductVM)
        {
            var brandBLL = new BrandBLL();
            var brandId = await brandBLL.CheckExistsId(updateProductVM.BrandId);
            if (brandId == false)
            {
                return false;
            }
            if (updateProductVM.CategoryIds != null)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
                {
                    var categories = await categoryBLL.CheckExistsId(updateProductVM.CategoryIds[i]);
                    if (categories == false)
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


            if (updateProductVM.Files != null)
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
                QuantityInStock = updateProductVM.QuantityInStock,
                Published = updateProductVM.Published,
                Deleted = updateProductVM.Deleted,
                UpdatedAt = DateTime.Now,
                BrandId = updateProductVM.BrandId,
            };

            #endregion

            //var listPC = new List<ProductCategoryVM>();





            var pictures = new List<PictureVM>();
            if (updateProductVM.Files != null)
            {
                var pictureBLL = new PictureBLL();
                for (int i = 0; i < updateProductVM.Files.Count; i++)
                {
                    var pictureId = cm.RandomString(16);
                    var checkPictureId = await pictureBLL.CheckExists(pictureId);
                    while (checkPictureId)
                    {
                        pictureId = cm.RandomString(16);
                        checkPictureId = await pictureBLL.CheckExists(pictureId);
                    }
                    var pictureVM = new PictureVM
                    {
                        Id = pictureId,
                        Name = updateProductVM.ImageNames[i],
                        ObjectId = id,
                        ObjectType = objectType,
                        Published = true,
                    };
                    pictures.Add(pictureVM);
                }


            }

            var listPC = new List<ProductCategoryVM>();
            for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
            {
                var pcmVM = new ProductCategoryVM
                {
                    CategoryId = updateProductVM.CategoryIds[i],
                    ProductId = id
                };
                listPC.Add(pcmVM);
            }

            var saveProduct = await productDAL.Update(productVM, pictures, listPC);
            if (saveProduct == false)
            {
                return false;
            }

            #region Product_Category
            //var pcmBLL = new ProductCategoryBLL();
            //var listProCatMapping = await pcmBLL.GetById(id, "ProductId");
            //if (listProCatMapping.Count > 0)
            //{
            //    var delete = await pcmBLL.Delete(id, "ProductId");

            //    if (delete)
            //    {
            //        if (updateProductVM.CategoryIds.Count > 0)
            //        {
            //            var listPC = new List<ProductCategoryVM>();
            //            for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
            //            {
            //                var pcmVM = new ProductCategoryVM
            //                {
            //                    CategoryId = updateProductVM.CategoryIds[i],
            //                    ProductId = id
            //                };
            //                listPC.Add(pcmVM);
            //            }
            //            var pcmCreate = await pcmBLL.Create(listPC);
            //            if (pcmCreate == false)
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //    updateProductVM.CategoryIds = null;
            //}
            ////updateProductVM.CategoryIds = new List<string>();
            //if (updateProductVM.CategoryIds != null)
            //{
            //    var listPC = new List<ProductCategoryVM>();
            //    for (int i = 0; i < updateProductVM.CategoryIds.Count; i++)
            //    {
            //        var pcmVM = new ProductCategoryVM
            //        {
            //            CategoryId = updateProductVM.CategoryIds[i],
            //            ProductId = id
            //        };
            //        listPC.Add(pcmVM);
            //    }
            //    var pcmCreate = await pcmBLL.Create(listPC);
            //    if (pcmCreate == false)
            //    {
            //        return false;
            //    }
            //}
            #endregion

            return true;
        }
        public async Task<bool> Delete(string id)
        {
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
            try
            {
                var resultFromDAL = await productDAL.ListProductCard();
                if (resultFromDAL == null)
                {
                    return null;
                }
                var cmtBLL = new ProductCmtBLL();
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

                    var productImageBLL = new PictureBLL();
                    var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                    if (listImg.Count > 0)
                    {
                        resultFromDAL[i].ImageName = listImg[0].Name;
                    }

                    var star = await cmtBLL.Star(resultFromDAL[i].Id);

                    if (star.Count() == 0)
                    {
                        resultFromDAL[i].Star = 0;
                    }
                    else
                    {
                        resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
                    }
                }
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }

        //public async Task<List<ProductCardVM>> ListProductCardNew(int take)
        //{
        //    try
        //    {
        //        var result = await ListProductCard();
        //        return result.Take(10).OrderBy(x=>x.cre).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public async Task<ProductCardVM> ProductCard(string id)
        {
            var resultFromDAL = await productDAL.ProductCard(id);
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

            var productImageBLL = new PictureBLL();
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
            if (listImg.Count > 0)
            {
                resultFromDAL.ImageName = listImg[0].Name;
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

            var productImageBLL = new PictureBLL();
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
            if (listImg.Count > 0)
            {
                resultFromDAL.ImageName = listImg[0].Name;
            }

            var cmtBLL = new ProductCmtBLL();
            var star = await cmtBLL.Star(resultFromDAL.Id);

            if (star.Count() == 0)
            {
                resultFromDAL.Star = 0;
            }
            else
            {
                resultFromDAL.Star = star.Sum(x => x.Value) / (float)star.Count();
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
            var cmtBLL = new ProductCmtBLL();
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

                var productImageBLL = new PictureBLL();
                var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                if (listImg.Count > 0)
                {
                    resultFromDAL[i].ImageName = listImg[0].Name;
                }

                var star = await cmtBLL.Star(resultFromDAL[i].Id);

                if (star.Count() == 0)
                {
                    resultFromDAL[i].Star = 0;
                }
                else
                {
                    resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
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


            var productImageBLL = new PictureBLL();
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
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

            if (model.BrandSlugs.Count > 0)
            {

                for (int i = 0; i < model.BrandSlugs.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var checkBrand = await GetById(resultFromDAL[j].Id);
                        if (checkBrand != null)
                        {
                            if (model.BrandSlugs[i] == checkBrand.BrandId)
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
            if (model.CategorySlugs.Count > 0)
            {
                for (int i = 0; i < model.CategorySlugs.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var checkCategory = await pcBLL.GetById(model.CategorySlugs[i], "CategoryId");
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
                //for (int i = 0; i < tempCategory.Products.Count; i++)
                //{
                //    for (int j = 0; j < tempCategory.Products.Count; j++)
                //    {
                //        if (i == j)
                //        {
                //            continue;
                //        }
                //        if (tempCategory.Products[i].Id == tempCategory.Products[j].Id)
                //        {
                //            tempCategory.Products.Remove(tempCategory.Products[j]);
                //        }
                //    }
                //}
                tempCategory.Products = tempCategory.Products.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            }

            var tempFinal = new ProductPaginationAdminVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductNameVM>(),
            };

            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count > 0)
            {
                var tempProduct = new List<ProductNameVM>();
                for (int j = 0; j < tempCategory.Products.Count; j++)
                {
                    for (int m = 0; m < model.BrandSlugs.Count; m++)
                    {
                        if (tempCategory.Products[j].BrandId == model.BrandSlugs[m])
                        {
                            tempProduct.Add(tempCategory.Products[j]);
                        }
                    }
                }
                tempFinal.Products = tempProduct;
            }

            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count > 0)
            {
                var countTemp = tempFinal.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempFinal.Products = tempFinal.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempFinal.TotalPage = totalPageTemp;
                tempFinal.TotalResult = countTemp;
                return tempFinal;
            }


            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count == 0)
            {
                var countTemp = tempBrand.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempBrand.Products = tempBrand.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempBrand.TotalPage = totalPageTemp;
                tempBrand.TotalResult = countTemp;

                return tempBrand;
            }

            if (model.CategorySlugs.Count > 0 && model.BrandSlugs.Count == 0)
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

            var productImageBLL = new PictureBLL();
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
            resultFromDAL.PictureVMs = listImg;

            var cmtBLL = new ProductCmtBLL();

            var star = await cmtBLL.Star(resultFromDAL.Id);

            if (star.Count() == 0)
            {
                resultFromDAL.Star = 0;
                resultFromDAL.StarCount = 0;
            }
            else
            {
                resultFromDAL.Star = star.Sum(x => x.Value) / (float)star.Count();
                resultFromDAL.StarCount = star.Count();
            }

            var wishlistBLL = new WishlistBLL();
            var wishlist = await wishlistBLL.Count(resultFromDAL.Id);

            resultFromDAL.Like = wishlist;

            return resultFromDAL;
        }

        public async Task<bool> CheckExists(string id)
        {
            return await productDAL.CheckExists(id);
        }
        public async Task<CartRowVM> CartRow(string id)
        {
            var resultFromDAL = await productDAL.CartRow(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            var productImageBLL = new PictureBLL();
            var brandBLL = new BrandBLL();

            var brand = await brandBLL.GetById(resultFromDAL.BrandId);
            if (brand != null)
            {
                resultFromDAL.BrandNameVM = new BrandNameVM();
                resultFromDAL.BrandNameVM.Id = brand.Id;
                resultFromDAL.BrandNameVM.Name = brand.Name;
                resultFromDAL.BrandNameVM.Slug = brand.Slug;
            }
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
            if (listImg.Count > 0)
            {
                resultFromDAL.ImageName = listImg[0].Name;
            }
            return resultFromDAL;
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

                        var productImageBLL = new PictureBLL();
                        var listImg = await productImageBLL.GetByObjectId(resultFromDb.Id, objectType);
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

        public async Task<List<ProductWidgetVM>> NewProductWidget()
        {
            var resultFromDAL = await productDAL.NewProductWidget();
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return resultFromDAL;
            }
            var productImageBLL = new PictureBLL();
            for (int i = 0; i < resultFromDAL.Count; i++)
            {
                var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                if (listImg.Count > 0)
                {
                    resultFromDAL[i].ImgName = listImg[0].Name;
                }
            }
            return resultFromDAL;
        }

        public async Task<ProductPaginationVM> ProductFilter(ProductFilterVM model)
        {

            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Search.ToLower();
            }
            var resultFromDAL = await productDAL.ProductFilter(model);
            if (resultFromDAL == null)
            {
                return null;
            }

            if (resultFromDAL.Count == 0)
            {
                return new ProductPaginationVM
                {
                    TotalPage = 0,
                    TotalResult = 0,
                    Products = new List<ProductCardVM>(),
                };
            }
            var cmtBLL = new ProductCmtBLL();
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

                var productImageBLL = new PictureBLL();
                var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                if (listImg.Count > 0)
                {
                    resultFromDAL[i].ImageName = listImg[0].Name;
                }

                var star = await cmtBLL.Star(resultFromDAL[i].Id);

                if (star.Count() == 0)
                {
                    resultFromDAL[i].Star = 0;
                }
                else
                {
                    resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
                }

            }

            var tempBrand = new ProductPaginationVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductCardVM>(),
            };


            if (model.BrandSlugs.Count > 0)
            {

                for (int i = 0; i < model.BrandSlugs.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var checkBrand = await ProductCard(resultFromDAL[j].Id);
                        if (checkBrand != null)
                        {
                            if (model.BrandSlugs[i] == checkBrand.BrandNameVM.Slug)
                            {
                                tempBrand.Products.Add(resultFromDAL[j]);
                            }
                        }
                    }
                }
            }

            var tempCategory = new ProductPaginationVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductCardVM>(),
            };
            var pcBLL = new ProductCategoryBLL();
            var categoryBLL = new CategoryBLL();
            if (model.CategorySlugs.Count > 0)
            {
                for (int i = 0; i < model.CategorySlugs.Count; i++)
                {
                    for (int j = 0; j < resultFromDAL.Count; j++)
                    {
                        var category = await categoryBLL.GetBySlug(model.CategorySlugs[i]);
                        var checkCategory = await pcBLL.GetById(category.Id, "CategoryId");
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
                //for (int i = 0; i < tempCategory.Products.Count; i++)
                //{
                //    for (int j = 0; j < tempCategory.Products.Count; j++)
                //    {
                //        if (i == j)
                //        {
                //            continue;
                //        }
                //        if (tempCategory.Products[i].Id == tempCategory.Products[j].Id)
                //        {
                //            tempCategory.Products.Remove(tempCategory.Products[j]);
                //        }
                //    }
                //}
                tempCategory.Products = tempCategory.Products.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            }

            var tempFinal = new ProductPaginationVM
            {
                TotalPage = 0,
                TotalResult = 0,
                Products = new List<ProductCardVM>(),
            };

            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count > 0)
            {
                var tempProduct = new List<ProductCardVM>();
                for (int j = 0; j < tempCategory.Products.Count; j++)
                {
                    for (int m = 0; m < model.BrandSlugs.Count; m++)
                    {
                        if (tempCategory.Products[j].BrandNameVM.Slug == model.BrandSlugs[m])
                        {
                            tempProduct.Add(tempCategory.Products[j]);
                        }
                    }
                }
                tempFinal.Products = tempProduct;
            }

            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count > 0)
            {
                var countTemp = tempFinal.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempFinal.Products = tempFinal.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempFinal.TotalPage = totalPageTemp;
                tempFinal.TotalResult = countTemp;
                return tempFinal;
            }


            if (model.BrandSlugs.Count > 0 && model.CategorySlugs.Count == 0)
            {
                var countTemp = tempBrand.Products.Count();
                var totalPageTemp = (int)Math.Ceiling(countTemp / (double)model.Limit);
                tempBrand.Products = tempBrand.Products.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
                tempBrand.TotalPage = totalPageTemp;
                tempBrand.TotalResult = countTemp;

                return tempBrand;
            }

            if (model.CategorySlugs.Count > 0 && model.BrandSlugs.Count == 0)
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

            var result = new ProductPaginationVM
            {
                TotalPage = totalPage,
                Products = resultFromDAL,
                TotalResult = count,
            };

            return result;
        }

        public async Task<ProductOrderVM> GetProductByOrderDetail(string id)
        {
            try
            {
                var resultFromDAL = await productDAL.GetProductByOrderDetail(id);
                if (resultFromDAL == null)
                {
                    return null;
                }
                var productImageBLL = new PictureBLL();
                var brandBLL = new BrandBLL();

                var brand = await brandBLL.GetById(resultFromDAL.BrandId);
                if (brand != null)
                {
                    resultFromDAL.BrandNameVM = new BrandNameVM();
                    resultFromDAL.BrandNameVM.Id = brand.Id;
                    resultFromDAL.BrandNameVM.Name = brand.Name;
                    resultFromDAL.BrandNameVM.Slug = brand.Slug;
                }
                var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, objectType);
                if (listImg.Count > 0)
                {
                    resultFromDAL.ImageName = listImg[0].Name;
                }



                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IncreaseView(string id)
        {
            try
            {
                return await productDAL.IncreaseView(id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductCardVM>> ProductWishlist(string userId)
        {
            try
            {
                var resultFromDAL = await productDAL.ProductWishlist(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<ProductCardVM>();
                }
                var cmtBLL = new ProductCmtBLL();
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

                    var productImageBLL = new PictureBLL();
                    var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                    if (listImg.Count > 0)
                    {
                        resultFromDAL[i].ImageName = listImg[0].Name;
                    }


                    var star = await cmtBLL.Star(resultFromDAL[i].Id);

                    if (star.Count() == 0)
                    {
                        resultFromDAL[i].Star = 0;
                    }
                    else
                    {
                        resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
                    }
                }
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> PublishedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await productDAL.PublishedTrueList(ids);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PublishedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await productDAL.PublishedFalseList(ids);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await productDAL.DeletedTrueList(ids);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await productDAL.DeletedFalseList(ids);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductCardVM>> MostBought(int take)
        {
            try
            {
                var resultFromDAL = await productDAL.MostBought();
                resultFromDAL = resultFromDAL.Take(8).ToList();
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<ProductCardVM>();
                }
                var cmtBLL = new ProductCmtBLL();
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

                    var productImageBLL = new PictureBLL();
                    var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                    if (listImg.Count > 0)
                    {
                        resultFromDAL[i].ImageName = listImg[0].Name;
                    }


                    var star = await cmtBLL.Star(resultFromDAL[i].Id);

                    if (star.Count() == 0)
                    {
                        resultFromDAL[i].Star = 0;
                    }
                    else
                    {
                        resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
                    }
                }
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductCardVM>> OnSale(int take)
        {
            try
            {
                var resultFromDAL = await productDAL.OnSale(take);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<ProductCardVM>();
                }
                var cmtBLL = new ProductCmtBLL();
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

                    var productImageBLL = new PictureBLL();
                    var listImg = await productImageBLL.GetByObjectId(resultFromDAL[i].Id, objectType);
                    if (listImg.Count > 0)
                    {
                        resultFromDAL[i].ImageName = listImg[0].Name;
                    }


                    var star = await cmtBLL.Star(resultFromDAL[i].Id);

                    if (star.Count() == 0)
                    {
                        resultFromDAL[i].Star = 0;
                    }
                    else
                    {
                        resultFromDAL[i].Star = star.Sum(x => x.Value) / (float)star.Count();
                    }
                }
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }
    
    }

}
