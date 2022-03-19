using BLL.ProductCategoryMapping;
using DAL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BO.ViewModels.Product;
using BO.ViewModels.ProductCategoryMapping;
using BLL.Category;

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
            var productVMs = await productDAL.GetAll();
            if (productVMs == null)
            {
                return null;
            }
            return productVMs;
        }
        public async Task<ProductVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var productVM = await productDAL.GetById(id);
            if (productVM == null)
            {
                return null;
            }
            return productVM;
        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var products = await productDAL.GetByBrandId(id);
            if (products == null)
            {
                return null;
            }
            return products;
        }
        public async Task<bool> Create(CreateProductVM createProductVM)
        {
            var brandBLL = new BrandBLL();
            var brandId = await brandBLL.GetById(createProductVM.BrandId);
            if (brandId == null)
            {
                return false;
            }

            if (createProductVM.ListCategoryId != null)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < createProductVM.ListCategoryId.Count; i++)
                {
                    var categories = await categoryBLL.GetById(createProductVM.ListCategoryId[i]);
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
            //slug = slug.Replace(" ", "-");

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
                IsActive = createProductVM.IsActive,
                Deleted = false,
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



            #region Product_Category_Mapping


            if (createProductVM.ListCategoryId != null)
            {
                for (int i = 0; i < createProductVM.ListCategoryId.Count; i++)
                {
                    var pcmVM = new ProductCategoryMappingVM
                    {
                        CategoryId = createProductVM.ListCategoryId[i],
                        ProductId = productId
                    };
                    var pcmBLL = new ProductCategoryMappingBLL();
                    var pcmCreate = await pcmBLL.Create(pcmVM);
                    if (pcmCreate == false)
                    {
                        return false;
                    }
                }
            }

            #endregion            

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
            if (updateProductVM.ListCategoryId != null)
            {
                var categoryBLL = new CategoryBLL();
                for (int i = 0; i < updateProductVM.ListCategoryId.Count; i++)
                {
                    var categories = await categoryBLL.GetById(updateProductVM.ListCategoryId[i]);
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
                IsActive = updateProductVM.IsActive,
                Deleted = updateProductVM.Deleted,
                UpdatedAt = DateTime.Now,
                BrandId = updateProductVM.BrandId,
            };



            #endregion

            #region Product_Category_Mapping
            var pcmBLL = new ProductCategoryMappingBLL();
            var listProCatMapping = await pcmBLL.GetById(id, "ProductId");
            if (listProCatMapping.Count != 0)
            {
                var delete = await pcmBLL.Delete(id, "ProductId");

                if (delete)
                {
                    if (updateProductVM.ListCategoryId != null)
                    {
                        for (int i = 0; i < updateProductVM.ListCategoryId.Count; i++)
                        {
                            var pcmVM = new ProductCategoryMappingVM
                            {
                                CategoryId = updateProductVM.ListCategoryId[i],
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
                updateProductVM.ListCategoryId = null;
            }
            if (updateProductVM.ListCategoryId != null)
            {
                for (int i = 0; i < updateProductVM.ListCategoryId.Count; i++)
                {
                    var pcmVM = new ProductCategoryMappingVM
                    {
                        CategoryId = updateProductVM.ListCategoryId[i],
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
                var pcmBLL = new ProductCategoryMappingBLL();
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
    }
}
