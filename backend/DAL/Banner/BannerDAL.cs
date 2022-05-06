using BO;
using BO.ViewModels.Banner;
using BO.ViewModels.Picture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Banner
{
    public class BannerDAL
    {
        private readonly AppDbContext db;
        public BannerDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Banners.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(CreateBannerVM model, PictureVM pictureVM)
        {
            try
            {
                var obj = new BO.Entities.Banner
                {
                    Id = model.Id,
                    Content = model.Content,
                    SubContent = model.SubContent,
                    Published = model.Published,
                    Deleted = false,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = null,
                };
                await db.Banners.AddAsync(obj);
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                var picture = new BO.Entities.Picture
                {
                    Id = pictureVM.Id,
                    Name = pictureVM.Name,
                    Published = pictureVM.Published,
                    ObjectId = pictureVM.ObjectId,
                    ObjectType = pictureVM.ObjectType,
                };

                await db.Pictures.AddAsync(picture);
                var resultPicture = await db.SaveChangesAsync();
                if (resultPicture == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(CreateBannerVM model, PictureVM pictureVM)
        {
            try
            {
                var resultFromDb = await db.Banners.SingleOrDefaultAsync(x => x.Id == model.Id);
                resultFromDb.Content = model.Content;
                resultFromDb.SubContent = model.SubContent;
                resultFromDb.Published = model.Published;
                resultFromDb.UpdatedAt = DateTime.Now;
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                if (pictureVM.Name != null)
                {
                    var pictureFromDb = await db.Pictures.SingleOrDefaultAsync(x => x.ObjectId == resultFromDb.Id);
                    pictureFromDb.Name = pictureVM.Name;

                    var resultPicture = await db.SaveChangesAsync();
                }
                return true;
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
                var resultFromDb = await db.Banners.SingleOrDefaultAsync(x => x.Id == id);
                db.Banners.Remove(resultFromDb);
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                var pictureFromDb = await db.Pictures.Where(x => x.ObjectId == id).Where(x => x.ObjectType == "banner").SingleOrDefaultAsync();
                db.Pictures.Remove(pictureFromDb);
                var resultPicture = await db.SaveChangesAsync();
                if (resultPicture == 0)
                {
                    return false;
                }


                return true;
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
                var resultFromDb = await db.Banners.SingleOrDefaultAsync(x => x.Id == id);
                resultFromDb.Published = !resultFromDb.Published;
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
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
                var resultFromDb = await db.Banners.SingleOrDefaultAsync(x => x.Id == id);
                resultFromDb.Deleted = !resultFromDb.Deleted;
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<BannerVM>> GetList(bool deleted, bool published)
        {
            try
            {
                //var resultFromDb = await db.Banners.Where(x => x.Deleted == deleted && x.Published == published).ToListAsync();
                var resultFromDb = await (from b in db.Banners
                                          join p in db.Pictures on b.Id equals p.ObjectId into list
                                          from p in list.DefaultIfEmpty()
                                          select new BannerVM
                                          {
                                              Id = b.Id,
                                              Published = b.Published,
                                              Deleted = b.Deleted,
                                              Content = b.Content,
                                              SubContent = b.SubContent,
                                              CreatedAt = b.CreatedAt,
                                              UpdatedAt = b.UpdatedAt,
                                              ImageName = p.Name,
                                              ImageSrc = null,
                                          }).ToListAsync();
                return resultFromDb;

            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BannerVM>> GetList(bool deleted, bool published, string query)
        {
            try
            {
                //var resultFromDb = await db.Banners.Where(x => x.Deleted == deleted && x.Published == published).ToListAsync();
                //if (!string.IsNullOrEmpty(query))
                //{
                //    resultFromDb=resultFromDb.Contains(x=>x.Con)
                //}
                var resultFromDb = await (from b in db.Banners
                                          join p in db.Pictures on b.Id equals p.ObjectId into list
                                          from p in list.DefaultIfEmpty()
                                          where b.Deleted==deleted
                                          select new BannerVM
                                          {
                                              Id = b.Id,
                                              Published = b.Published,
                                              Deleted = b.Deleted,
                                              Content = b.Content,
                                              SubContent = b.SubContent,
                                              CreatedAt = b.CreatedAt,
                                              UpdatedAt = b.UpdatedAt,
                                              ImageName = p.Name,
                                              ImageSrc = null,
                                          }).ToListAsync();
                
                return resultFromDb;

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
                return await (from b in db.Banners.Where(x=>x.Id==id)
                              join p in db.Pictures on b.Id equals p.ObjectId into list
                              from p in list.DefaultIfEmpty()
                              select new BannerVM
                              {
                                  Id = b.Id,
                                  Content = b.Content,
                                  SubContent = b.SubContent,
                                  Published = b.Published,
                                  Deleted = b.Deleted,
                                  CreatedAt = b.CreatedAt,
                                  UpdatedAt = b.UpdatedAt,
                                  ImageName = p.Name,
                                  ImageSrc = null
                              }).SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
