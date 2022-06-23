using BO;
using BO.ViewModels.Picture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Picture
{
    public class PictureDAL
    {
        private readonly AppDbContext db;
        public PictureDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Pictures.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<bool> Create(List<PictureVM> obj)
        {
            var imgs = obj.Select(x => new BO.Entities.Picture
            {
                Id = x.Id,
                Name = x.Name,
                ObjectId = x.ObjectId,
                Published = x.Published,
            });
            await db.Pictures.AddRangeAsync(imgs);
            var result = await db.SaveChangesAsync();
            if (result >= imgs.Count())
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var imgFormDb = await db.Pictures.SingleOrDefaultAsync(x => x.Id == id);
            if (imgFormDb == null)
            {
                return false;
            }
            db.Pictures.Remove(imgFormDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool pulished)
        {
            var productImage = await db.Pictures.SingleOrDefaultAsync(x => x.Id == id);

            productImage.Published = pulished;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<PictureVM> GetById(string id)
        {
            try
            {
                var resultFromDb = await db.Pictures.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                return new PictureVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    ObjectId = resultFromDb.ObjectId,
                    Published = resultFromDb.Published,
                    Order=resultFromDb.Order,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<PictureVM>> GetByObjectId(string objectId, string objectType)
        {
            try
            {
                var resultFromDb = await db.Pictures.Where(x => x.ObjectId == objectId).Where(x => x.ObjectType == objectType).OrderBy(x=>x.Order).ToListAsync();
                if (resultFromDb .Count==0)
                {
                    return new List<PictureVM>();
                }
                var result = resultFromDb.Select(x => new PictureVM
                {
                    Id = x.Id,
                    ObjectType = objectType,
                    Name = x.Name,
                    Published = x.Published,
                    ObjectId = x.ObjectId,
                    Order=x.Order,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    
        //public async Task<bool> ChangeOrder(string id, int order)
        //{
        //    try
        //    {

        //    }
        //    catch
        //    {

        //    }
        //}
    }
}
