using BLL.BrandImage;
using BO.ViewModels.Brand;
using BO.ViewModels.BrandImage;
using DAL.Brand;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace BLL
{
    public class BrandBLL
    {
        private BrandDAL brandDAL;
        private BrandImageBLL brandImageBLL;
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


            var brandVM = new BrandVM
            {
                Id = brandId,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                Pulished = model.Pulished,
                Deleted = false,
                CreatedAt = DateTime.Now,
            };

            var saveBrand = await brandDAL.Create(brandVM);
            if (!saveBrand)
            {
                return false;
            }

            if (model.Files.Count > 0)
            {
                brandImageBLL = new BrandImageBLL();
                var saveImg = await brandImageBLL.Create(model.ImageNames, brandId);
                if (!saveImg)
                {
                    return false;
                }
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



            var brandVM = new BrandVM
            {
                Id = id,
                Name = model.Name,
                Slug = slug,
                FullDescription = model.FullDescription,
                ShortDescription = model.ShortDescription,
                Pulished = model.Pulished,
                Deleted = model.Deleted,
                UpdatedAt = DateTime.Now,
                Ordinal = model.Ordinal
            };

            var saveBrand = await brandDAL.Update(brandVM);
            if (!saveBrand)
            {
                return false;
            }

            if (model.Files.Count > 0)
            {
                brandImageBLL = new BrandImageBLL();
                var saveImg = await brandImageBLL.Create(model.ImageNames, id);
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
            var brandVM = await GetById(id);
            if (brandVM == null)
            {
                return false;
            }
            return await brandDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var brandVM = await GetById(id);
            if (brandVM == null)
            {
                return false;
            }
            bool pulished = !brandVM.Pulished;
            var result = await brandDAL.Pulished(id, pulished);
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
            var result = await brandDAL.Deleted(id, deleted);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<List<BrandVM>> GetAllBrandDeleted(bool deleted)
        {
            return await brandDAL.GetAllBrandDeleted(deleted);
        }
    }
}
