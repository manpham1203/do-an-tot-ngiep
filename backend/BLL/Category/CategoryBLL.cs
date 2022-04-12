﻿using BLL.CategoryImage;
using BLL.Product;
using BLL.ProductCategory;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using DAL.Category;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Category
{
    public class CategoryBLL
    {
        private readonly CategoryDAL categoryDAL;
        private CommonBLL cm;
        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            return await categoryDAL.GetAll();
        }
        public async Task<CategoryVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            return await categoryDAL.GetById(id);
        }
        public async Task<bool> Create(CreateCategoryVM model)
        {
            cm = new CommonBLL();
            var categoryId = cm.RandomString(12);
            var checkIdExists = await GetById(categoryId);
            while (checkIdExists != null)
            {
                categoryId = cm.RandomString(12);
                checkIdExists = await GetById(categoryId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            if (model.Files.Count > 0)
            {
                model.ImageNames = new List<string>();
                for (int i = 0; i < model.Files.Count; i++)
                {
                    string imageName = slug;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.Files[i].FileName);
                    model.ImageNames.Add(imageName);
                    Thread.Sleep(200);
                }
            }

            var categoryVM = new CategoryVM
            {
                Id = categoryId,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                Published = model.Published,
                Deleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Ordinal = model.Ordinal,
            };
            var saveCategory = await categoryDAL.Create(categoryVM);
            if (saveCategory == false)
            {
                return false;
            }

            if (model.Files.Count > 0)
            {
                var categoryImageBLL = new CategoryImageBLL();
                var saveImg = await categoryImageBLL.Create(model.ImageNames, categoryId);
                if (!saveImg)
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<bool> Update(string id, UpdateCategoryVM model)
        {
            cm = new CommonBLL();
            var checkCategory = await GetById(id);
            if (checkCategory == null)
            {
                return false;
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            if (model.Files.Count > 0)
            {
                model.ImageNames = new List<string>();
                for (int i = 0; i < model.Files.Count; i++)
                {
                    string imageName = slug;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.Files[i].FileName);
                    model.ImageNames.Add(imageName);
                    Thread.Sleep(200);
                }
            }

            var categoryVM = new CategoryVM
            {
                Id = id,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                Published = model.Published,
                Deleted = model.Deleted,
                UpdatedAt = DateTime.Now,
                Ordinal = model.Ordinal,
            };

            var saveCategory = await categoryDAL.Update(categoryVM);
            if (saveCategory == false)
            {
                return false;
            }

            if (model.Files.Count > 0)
            {
                var categoryImageBLL = new CategoryImageBLL();
                var saveImg = await categoryImageBLL.Create(model.ImageNames, id);
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
            var categoryFullBLL = new CategoryFullBLL();
            var categoryFullVM = await categoryFullBLL.GetById(id);
            if (categoryFullVM == null)
            {
                return false;
            }
            if (categoryFullVM.ProductVMs != null)
            {
                var pcmBLL = new ProductCategoryBLL();
                var listProCatMapping = await pcmBLL.GetById(id, "CategoryId");
                if (listProCatMapping != null)
                {
                    var delete = await pcmBLL.Delete(id, "CategoryId");
                    if (delete == false)
                    {
                        return false;
                    }
                }
            }

            return await categoryDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var categoryVM = await GetById(id);
            if (categoryVM == null)
            {
                return false;
            }
            bool published = !categoryVM.Published;
            var result = await categoryDAL.Pulished(id, published);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id)
        {
            var categoryVM = await GetById(id);
            if (categoryVM == null)
            {
                return false;
            }
            bool deleted = !categoryVM.Deleted;
            var result = await categoryDAL.Deleted(id, deleted);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<List<CategoryVM>> GetAllCategoryDeleted(bool deleted)
        {
            return await categoryDAL.GetAllCategoryDeleted(deleted);
        }

        public async Task<List<CategoryNameVM>> AllCategoryWithProductCard()
        {
            var resultFromDAL = await categoryDAL.AllCategoryName();
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<CategoryNameVM>();
            }
            if (resultFromDAL.Count > 0)
            {
                var productBLL = new ProductBLL();
                var pcBLL = new ProductCategoryBLL();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    var listPC = await pcBLL.GetById(resultFromDAL[i].Id, "CategoryId");
                    resultFromDAL[i].ProductCardVMs = new List<ProductCardVM>();
                    if (listPC.Count > 0)
                    {
                        for (int j = 0; j < listPC.Count(); j++)
                        {
                            var productCard = await productBLL.ProductCardById(listPC[j].ProductId);
                            if (productCard != null)
                            {
                                resultFromDAL[i].ProductCardVMs.Add(productCard);
                            }
                        }
                    }



                }
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    if (resultFromDAL[i].ProductCardVMs.Count == 0)
                    {
                        resultFromDAL.Remove(resultFromDAL[i]);
                    }
                }
            }

            return resultFromDAL;
        }

        public async Task<List<CategoryNameVM>> AllCategoryName()
        {
            var resultFromDAL = await categoryDAL.AllCategoryName();
            if (resultFromDAL.Count == 0)
            {
                return new List<CategoryNameVM>();
            }
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }
        public async Task<List<CategoryNameVM>> AllCategoryName(bool deleted)
        {
            var resultFromDAL = await categoryDAL.AllCategoryName(deleted);
            if (resultFromDAL.Count == 0)
            {
                return new List<CategoryNameVM>();
            }
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }
        public async Task<CategoryRowAdminVM> BrandRowAdmin(string id)
        {
            var resultFromDAL = await categoryDAL.CategoryRowAdmin(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }
        public async Task<CategoryNameVM> CategoryNameById(string id)
        {
            var resultFromDAL = await categoryDAL.CategoryNameById(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }

        public async Task<CategoryRowAdminVM> CategoryRowAdmin(string id)
        {
            var resultFromDAL = await categoryDAL.CategoryRowAdmin(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }


    }
}
