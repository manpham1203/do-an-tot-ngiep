using BO.ViewModels.CategoryImage;
using DAL.CategoryImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CategoryImage
{
    public class CategoryImageBLL
    {
        private CategoryImageDAL categoryImageDAL;
        private CommonBLL cm;

        public CategoryImageBLL()
        {
            categoryImageDAL = new CategoryImageDAL();
        }
        public async Task<CategoryImageVM> GetById(string id)
        {
            return await categoryImageDAL.GetById(id);
        }
        public async Task<List<CategoryImageVM>> GetByCategoryId(string id)
        {
            return await categoryImageDAL.GetByCategoryId(id);
        }
        public async Task<bool> Create(List<string> imgName, string categoryId)
        {
            cm = new CommonBLL();
            List<CategoryImageVM> categoryImageVMs = new List<CategoryImageVM>();
            CategoryImageVM categoryImageVM;
            for (int i = 0; i < imgName.Count; i++)
            {
                var imgId = cm.RandomString(12);
                var checkImg = await GetById(imgId);
                while (checkImg != null)
                {
                    imgId = cm.RandomString(12);
                    checkImg = await GetById(categoryId);
                }
                categoryImageVM = new CategoryImageVM
                {
                    Id = imgId,
                    CategoryId = categoryId,
                    Name = imgName[i],
                    Published = true,
                };

                categoryImageVMs.Add(categoryImageVM);
            }
            return await categoryImageDAL.Create(categoryImageVMs);
        }
        public async Task<bool> Delete(string id)
        {
            var categoryImage = await GetById(id);
            if (categoryImage == null)
            {
                return false;
            }
            return await categoryImageDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var categoryImageVM = await GetById(id);
            if (categoryImageVM == null)
            {
                return false;
            }
            bool published = !categoryImageVM.Published;
            var result = await categoryImageDAL.Pulished(id, published);
            if (result)
            {
                return true;
            }
            return false;
        }

    }
}
