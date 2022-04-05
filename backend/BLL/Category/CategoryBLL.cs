using BLL.ProductCategory;
using BO.ViewModels.Category;
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
                Pulished = model.Pulished,
                Deleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt=null,
                Ordinal = model.Ordinal,
            };

            return await categoryDAL.Create(categoryVM);
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
                Pulished = model.Pulished,
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
    }
}
