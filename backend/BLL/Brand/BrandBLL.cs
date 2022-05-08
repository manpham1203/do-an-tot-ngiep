using BLL.Picture;
using BLL.Product;
using BO.ViewModels.Brand;
using BO.ViewModels.Picture;
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
        private CommonBLL cm;
        private string objectType="brand";

        public BrandBLL()
        {
            brandDAL = new BrandDAL();
        }

        //public async Task<List<BrandVM>> GetAll()
        //{
        //    return await brandDAL.GetAll();
        //}
        public async Task<BrandVM> GetById(string id)
        {
            return await brandDAL.GetById(id);
        }
        //public async Task<BrandVM> GetBySlug(string slug)
        //{
        //    return await brandDAL.GetBySlug(slug);
        //}
        public async Task<bool> CheckExistsId(string id)
        {
            try
            {
                return await brandDAL.CheckExistsId(id);
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
                return await brandDAL.CheckExistsSlug(slug);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(CreateBrandVM model)
        {

            cm = new CommonBLL();
            var brandId = cm.RandomString(6);
            var checkIdExists = await CheckExistsId(brandId);
            while (checkIdExists)
            {
                brandId = cm.RandomString(6);
                checkIdExists = await CheckExistsId(brandId);
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Name).Trim().ToLower(), @"\s+", "-");


            if (model.File != null)
            {
                string imageName = slug;
                imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                model.ImageName = imageName;
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
                ObjectId = brandId,
                ObjectType= objectType,
                Published = true,
            };

            var saveBrand = await brandDAL.Create(brandVM, pictureVM);
            if (!saveBrand)
            {
                return false;
            }


            return true;

        }
        public async Task<bool> Update(string id, UpdateBrandVM model)
        {
            cm = new CommonBLL();
            var checkBrand = await CheckExistsId(id);
            if (checkBrand == false)
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
            };

            var pictureVM = new PictureVM
            {
                Name = model.ImageName,
                ObjectId = id,
                Published = true,
            };

            var saveBrand = await brandDAL.Update(brandVM, pictureVM);
            if (!saveBrand)
            {
                return false;
            }



            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var brandVM = await CheckExistsId(id);
            if (brandVM == false)
            {
                return false;
            }
            return await brandDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var brandVM = await CheckExistsId(id);
            if (brandVM == false)
            {
                return false;
            }
            var result = await brandDAL.Pulished(id);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id)
        {
            var brandVM = await CheckExistsId(id);
            if (brandVM == false)
            {
                return false;
            }
            var result = await brandDAL.Deleted(id);
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
                    if (resultFromDAL[i].ProductCardVMs.Count == 0)
                    {
                        resultFromDAL.Remove(resultFromDAL[i]);
                    }
                }
            }
            return resultFromDAL;
        }

        public async Task<BrandNameVM> BrandWithProductCard(string id)
        {
            var resultFromDAL = await brandDAL.BrandWithProductCard(id);
            if (resultFromDAL == null)
            {
                return null;
            }

            var productBLL = new ProductBLL();

            resultFromDAL.ProductCardVMs = new List<ProductCardVM>();
            var productCards = await productBLL.ListProductCardOfBrand(resultFromDAL.Id);
            resultFromDAL.ProductCardVMs = productCards;

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

        public async Task<BrandPaginationAdminVM> AllBrandNameAdmin(bool deleted, BrandFilterVM model)
        {
            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Search.ToLower();
            }
            var resultFromDAL = await brandDAL.AllBrandNameAdmin(deleted, model);
            if (resultFromDAL == null)
            {
                return null;
            }
            if (resultFromDAL.Count == 0)
            {
                return new BrandPaginationAdminVM
                {
                    TotalPage = 0,
                    TotalResult = 0,
                    Brands = new List<BrandNameVM>(),
                };
            }
            var count = resultFromDAL.Count();
            var totalPage = (int)Math.Ceiling(count / (double)model.Limit);
            resultFromDAL = resultFromDAL.Skip((model.CurrentPage - 1) * model.Limit).Take(model.Limit).ToList();
            return new BrandPaginationAdminVM
            {
                TotalResult = count,
                TotalPage = totalPage,
                Brands = resultFromDAL,

            };
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
