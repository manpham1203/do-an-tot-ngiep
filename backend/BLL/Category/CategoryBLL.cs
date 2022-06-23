using BLL.Picture;
using BLL.Product;
using BLL.ProductCategory;
using BO.ViewModels.Category;
using BO.ViewModels.Picture;
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
        private string objectType = "category";
        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();
        }
        //public async Task<List<CategoryVM>> GetAll()
        //{
        //    return await categoryDAL.GetAll();
        //}
        public async Task<CategoryVM> GetById(string id)
        {
            return await categoryDAL.GetById(id);
        }
        public async Task<CategoryVM> GetBySlug(string slug)
        {
            return await categoryDAL.GetBySlug(slug);
        }
        public async Task<bool> CheckExistsId(string id)
        {
            try
            {
                return await categoryDAL.CheckExistsId(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CheckExistsSlug(string slug)
        {
            try
            {
                return await categoryDAL.CheckExistsSlug(slug);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(CreateCategoryVM model)
        {
            cm = new CommonBLL();
            var categoryId = cm.RandomString(6);
            var checkIdExists = await CheckExistsId(categoryId);
            while (checkIdExists)
            {
                categoryId = cm.RandomString(6);
                checkIdExists = await CheckExistsId(categoryId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            if (model.File != null)
            {
                string imageName = slug;
                imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                model.ImageName = imageName;
            }

            var categoryVM = new CategoryVM
            {
                Id = categoryId,
                Name = model.Name,
                Slug = slug,
                Published = model.Published,
                Deleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };

            var pictureBLL = new PictureBLL();
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
                Name = model.ImageName,
                ObjectId = categoryId,
                Published = true,
                ObjectType = objectType,
            };

            var saveCategory = await categoryDAL.Create(categoryVM, pictureVM);
            if (saveCategory == false)
            {
                return false;
            }



            return true;
        }
        public async Task<bool> Update(string id, UpdateCategoryVM model)
        {
            cm = new CommonBLL();
            var checkCategory = await CheckExistsId(id);
            if (checkCategory == false)
            {
                return false;
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            if (model.File != null)
            {
                string imageName = slug;
                imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                model.ImageName = imageName;
            }

            var categoryVM = new CategoryVM
            {
                Id = id,
                Name = model.Name,
                Slug = slug,
                Published = model.Published,
                Deleted = model.Deleted,
                UpdatedAt = DateTime.Now,
            };

            var pictureVM = new PictureVM
            {
                Name = model.ImageName,
                ObjectId = id,
                Published = true,
            };

            var saveCategory = await categoryDAL.Update(categoryVM, pictureVM);
            if (saveCategory == false)
            {
                return false;
            }

            return true;

        }
        public async Task<bool> Delete(string id)
        {
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
            var categoryVM = await CheckExistsId(id);
            if (categoryVM == false)
            {
                return false;
            }
            var result = await categoryDAL.Pulished(id);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id)
        {
            var categoryVM = await CheckExistsId(id);
            if (categoryVM == false)
            {
                return false;
            }
            var result = await categoryDAL.Deleted(id);
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
                    listPC=listPC.Take(10).ToList();
                    resultFromDAL[i].ProductCardVMs = new List<ProductCardVM>();
                    if (listPC != null)
                    {
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

                }
            }

            return resultFromDAL.Where(x => x.ProductCardVMs.Count() != 0).ToList();
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


        public async Task<CategoryPaginationAdminVM> AllCategoryNameAdmin(bool deleted, CategoryFilterVM model)
        {
            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Search.ToLower();
            }
            var resultFromDAL = await categoryDAL.AllCategoryNameAdmin(deleted, model);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new CategoryPaginationAdminVM
                {
                    TotalPage = 0,
                    TotalResult = 0,
                    Categories = new List<CategoryNameVM>(),
                };
            }
            var count = resultFromDAL.Count();
            var totalPage = (int)Math.Ceiling(count / (double)model.Limit);
            resultFromDAL = resultFromDAL.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
            return new CategoryPaginationAdminVM
            {
                TotalResult = count,
                TotalPage = totalPage,
                Categories = resultFromDAL,

            };
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
        public async Task<CategoryVM> CategoryDetail(string id)
        {
            try
            {
                return await categoryDAL.CategoryDetail(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CategoryChartVM>> CategoryChart()
        {
            try
            {
                return await categoryDAL.CategoryChart();
            }
            catch
            {
                return null;
            }
        }
    }
}
