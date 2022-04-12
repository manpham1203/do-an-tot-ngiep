using BLL.BrandImage;
using BLL.Product;
using BO.ViewModels.Brand;
using BO.ViewModels.BrandImage;
using BO.ViewModels.Product;
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
        private CommonBLL cm;

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

            cm = new CommonBLL();
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
                Published = model.Published,
                Deleted = false,
                CreatedAt = DateTime.Now,
                Ordinal = model.Ordinal,
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
            cm = new CommonBLL();
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
                Published = model.Published,
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
            bool published = !brandVM.Published;
            var result = await brandDAL.Pulished(id, published);
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

        public async Task<List<BrandNameVM>> AllBrandWithProductCard()
        {
            var resultFromDAL = await brandDAL.AllBrandName();
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<BrandNameVM>();
            }
            if (resultFromDAL.Count > 0)
            {
                var productBLL = new ProductBLL();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    resultFromDAL[i].ProductCardVMs = new List<ProductCardVM>();
                    var productCards = await productBLL.ListProductCardOfBrand(resultFromDAL[i].Id);
                    resultFromDAL[i].ProductCardVMs = productCards;
                }
                for(int i=0; i < resultFromDAL.Count; i++)
                {
                    if (resultFromDAL[i].ProductCardVMs.Count == 0)
                    {
                        resultFromDAL.Remove(resultFromDAL[i]);
                    }
                }
            }
            return resultFromDAL;
        }

        public async Task<List<BrandNameVM>> AllBrandName()
        {
            var resultFromDAL = await brandDAL.AllBrandName();
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<BrandNameVM>();
            }
            return resultFromDAL;
        }

        public async Task<List<BrandNameVM>> AllBrandName(bool deleted)
        {
            var resultFromDAL = await brandDAL.AllBrandName(deleted);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new List<BrandNameVM>();
            }
            return resultFromDAL;
        }

        public async Task<BrandRowAdminVM> BrandRowAdmin(string id)
        {
            var resultFromDAL = await brandDAL.BrandRowAmin(id);
            if (resultFromDAL == null)
            {
                return null;
            }
            return resultFromDAL;
        }

    }
}
