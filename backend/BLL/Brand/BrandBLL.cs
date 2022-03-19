using BO.ViewModels.Brand;
using DAL.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class BrandBLL
    {
        private readonly BrandDAL brandDAL;
        private Common cm;
        public BrandBLL()
        {
            brandDAL = new BrandDAL();
        }
        public async Task<List<BrandVM>> GetAll()
        {
            return await brandDAL.GetAll();
        }
        public async Task<BrandVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            return await brandDAL.GetById(id);
        }
        public async Task<bool> Create(CreateBrandVM model)
        {
            cm = new Common();
            var brandId = cm.RandomString(12);
            var checkIdExists = await GetById(brandId);
            while (checkIdExists != null)
            {
                brandId = cm.RandomString(12);
                checkIdExists = await GetById(brandId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            var brandVM = new BrandVM
            {
                Id = brandId,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                IsActive = model.IsActive,
                Deleted = false,
                CreatedAt = DateTime.Now,
            };

            var brandCreate = await brandDAL.Create(brandVM);
            if (!brandCreate)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Update(string id, UpdateBrandVM model)
        {
            cm = new Common();
            var checkBrand = await GetById(id);
            if (checkBrand == null)
            {
                return false;
            }

            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");

            var brandVM = new BrandVM
            {
                Id = id,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                IsActive = model.IsActive,
                Deleted = model.Deleted,
                UpdatedAt=DateTime.Now,
                Ordinal=model.Ordinal
            };

            return await brandDAL.Update(brandVM);
        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var brandVM = await GetById(id);
            if (brandVM == null)
            {
                return false;
            }
            return await brandDAL.Delete(id);
        }
    }
}
