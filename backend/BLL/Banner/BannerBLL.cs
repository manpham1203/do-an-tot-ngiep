using BLL.Picture;
using BO.ViewModels.Banner;
using BO.ViewModels.Picture;
using DAL.Banner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BLL.Banner
{
    public class BannerBLL
    {
        private BannerDAL bannerDAL;
        private CommonBLL cm;
        public BannerBLL()
        {
            bannerDAL = new BannerDAL();
        }
        public async Task<bool> SaveFile(IFormFile file, string imgName)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", imgName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return true;
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await bannerDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(CreateBannerVM model)
        {
            try
            {
                cm = new CommonBLL();
                var id = cm.RandomString(6);
                var checkExists = await CheckExists(id);
                if (checkExists)
                {
                    id = cm.RandomString(6);
                    checkExists = await CheckExists(id);
                }
                model.Id = id;
                model.CreatedAt = DateTime.Now;
                var fileName = Regex.Replace(cm.RemoveUnicode(model.Content).Trim().ToLower(), @"\s+", "");
                if (model.File != null)
                {
                    string imageName = fileName;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                    model.ImageName = imageName;
                }
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
                    ObjectId = id,
                    ObjectType = "banner",
                    Published = true,
                };
                var a= await SaveFile(model.File, model.ImageName);
                if(a != true)
                {
                    return true;
                }
                return await bannerDAL.Create(model, pictureVM);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(string id, CreateBannerVM model)
        {
            try
            {
                cm = new CommonBLL();
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }
                model.Id = id;
                var fileName = Regex.Replace(cm.RemoveUnicode(model.Content).Trim().ToLower(), @"\s+", "");
                if (model.File != null)
                {
                    string imageName = fileName;
                    imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                    model.ImageName = imageName;
                }
                var pictureVM = new PictureVM
                {
                    Name = model.ImageName,
                    ObjectId = id,
                    Published = true,
                };
                return await bannerDAL.Update(model, pictureVM);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await bannerDAL.Delete(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Published(string id)
        {
            try
            {
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await bannerDAL.Published(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Deleted(string id)
        {
            try
            {
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await bannerDAL.Deleted(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<BannerVM>> GetList(bool deleted, bool published, string query = null)
        {
            try
            {
                return await bannerDAL.GetList(deleted, published, query);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BannerVM>> GetList(bool deleted, bool published)
        {
            try
            {
                return await bannerDAL.GetList(deleted, published);
            }
            catch
            {
                return null;
            }
        }

        public async Task<BannerPaginationVM> BannerPagination(int currentPage, int limit, string query, bool deleted)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = query.ToLower();
                }
                var list = await GetList(deleted, true, query);
                if (!string.IsNullOrEmpty(query))
                {
                    list = list.Where(x => x.Content.ToLower().Contains(query)).ToList();
                }
                var count = list.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                list = list.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new BannerPaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    BannerId = list.Select(x => x.Id).ToList(),

                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<BannerVM> GetById(string id)
        {
            try
            {
                return await bannerDAL.GetById(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
