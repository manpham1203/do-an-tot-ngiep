using BLL.ProductCategoryMapping;
using BOL.ViewModels.Category;
using DAL.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Category
{
    public class CategoryBLL
    {
        private readonly CategoryDAL categoryDAL;
        private Common cm;
        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            var categoryVMs = await categoryDAL.GetAll();
            if (categoryVMs == null)
            {
                return null;
            }

            return categoryVMs;
        }
        public async Task<CategoryVM> GetById(string id)
        {
            var categoryVM = await categoryDAL.GetById(id);
            if (categoryVM == null)
            {
                return null;
            }

            return categoryVM;
        }
        public async Task<bool> Create(CreateCategoryVM model)
        {
            cm = new Common();
            var categoryId = cm.RandomString(12);
            var checkIdExists = await GetById(categoryId);
            while (checkIdExists != null)
            {
                categoryId = cm.RandomString(12);
                checkIdExists = await GetById(categoryId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            var categoryVM = new CategoryVM
            {
                Id = categoryId,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                IsActive = model.IsActive,
                Deleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt=null,
                Ordinal = model.Ordinal,
            };

            var categoryCreate = await categoryDAL.Create(categoryVM);
            if (!categoryCreate)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Update(string id, UpdateCategoryVM model)
        {
            cm = new Common();
            var checkCategory = await GetById(id);
            if (checkCategory == null)
            {
                return false;
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");
            var categoryVM = new CategoryVM
            {
                Id = id,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                IsActive = model.IsActive,
                Deleted = model.Deleted,
                UpdatedAt = DateTime.Now,
                Ordinal = model.Ordinal,
            };

            return await categoryDAL.Update(categoryVM);

        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var categoryFullBLL=new CategoryFullBLL();
            var categoryFullVM = await categoryFullBLL.GetById(id);
            if (categoryFullVM == null)
            {
                return false;
            }
            if(categoryFullVM.ProductVMs != null)
            {
                var pcmBLL = new ProductCategoryMappingBLL();
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
    }
}
