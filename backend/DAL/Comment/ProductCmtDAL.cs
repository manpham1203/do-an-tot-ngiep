using BO;
using BO.ViewModels.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Comment
{
    public class ProductCmtDAL
    {
        private readonly AppDbContext db;
        public ProductCmtDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Comments.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                return true;

            }
            catch
            {
                return true;
            }
        }
        
        public async Task<bool> Create(ProductCmtVM model)
        {
            try
            {
                var obj = new BO.Entities.Comment
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Content = model.Content,
                    Star = model.Star,
                    ObjectId = model.ObjectId,
                    ObjectType = model.ObjectType,
                    OrderDetailId = model.OrderDetailId,
                    CreatedAt = model.CreatedAt,
                    ParentId = model.ParentId,
                };
                await db.Comments.AddAsync(obj);
                var result = await db.SaveChangesAsync();
                if (result == 0)
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

        public async Task<ProductCmtVM> CommentItem(string id)
        {
            try
            {
                //var resultFromDb = await db.Comments.SingleOrDefaultAsync(x=>x.Id==id);
                //var resultFromDb = await (from c in db.Comments
                //                          join p in db.Pictures on c.UserId equals p.ObjectId
                //                          select new ProductCmtVM
                //                          {
                //                              Id = c.Id,
                //                              UserId = c.UserId,
                //                              Content = c.Content,
                //                              ObjectId = c.ObjectId,
                //                              ObjectType = c.ObjectType,
                //                              OrderDetailId = c.OrderDetailId,
                //                              Star = c.Star,
                //                              CreatedAt = c.CreatedAt,
                //                              Image = p.Name
                //                          }).SingleOrDefaultAsync();

                //var resultFromDb = await db.Comments
                //    .Where(x => x.Id == id)
                //    .GroupJoin(db.Pictures,
                //    cmt => cmt.UserId,
                //    pic => pic.ObjectId,
                //    (x, y) => new { cmt = x, pic = y })
                //    .SelectMany(xy => xy.pic.DefaultIfEmpty(), (x, y) => new { cmt = x.cmt, pic = y })
                //    .Select(m => new ProductCmtVM
                //    {
                //        Id = m.cmt.Id,
                //        UserId = m.cmt.UserId,
                //        Content = m.cmt.Content,
                //        ObjectId = m.cmt.ObjectId,
                //        ObjectType = m.cmt.ObjectType,
                //        OrderDetailId = m.cmt.OrderDetailId,
                //        Star = m.cmt.Star,
                //        CreatedAt = m.cmt.CreatedAt,
                //        ImageName = m.pic.Name,
                //        ImageSrc = null
                //    }).SingleOrDefaultAsync();

                var resultFromDb = await (from cmt in db.Comments
                                          join pic in db.Pictures on cmt.UserId equals pic.ObjectId into list
                                          from pic in list.DefaultIfEmpty()
                                          join u in db.Users on cmt.UserId equals u.Id
                                          where cmt.Id == id
                                          select new ProductCmtVM
                                          {
                                              Id = cmt.Id,
                                              UserId = cmt.UserId,
                                              Content = cmt.Content,
                                              ObjectId = cmt.ObjectId,
                                              ObjectType = cmt.ObjectType,
                                              OrderDetailId = cmt.OrderDetailId,
                                              Star = cmt.Star,
                                              CreatedAt = cmt.CreatedAt,
                                              ImageName = pic.Name,
                                              ImageSrc = null,
                                              FullName = u.LastName + " " + u.FirstName,
                                              Children=null,
                                          }).SingleOrDefaultAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                //var result = new ProductCmtVM
                //{
                //    Id = resultFromDb.Id,
                //    UserId = resultFromDb.UserId,
                //    Content = resultFromDb.Content,
                //    ObjectId = resultFromDb.ObjectId,
                //    ObjectType = resultFromDb.ObjectType,
                //    OrderDetailId = resultFromDb.OrderDetailId,
                //    Star = resultFromDb.Star,
                //    CreatedAt = resultFromDb.CreatedAt,
                //};
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<string>> IdsOfProduct(string productId)
        {
            try
            {
                var resultFromDb = await db.Comments.OrderByDescending(x => x.CreatedAt).Where(x => x.ObjectId == productId && x.ObjectType == "product" && x.ParentId == null).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<string>();
                }
                var result = resultFromDb.Select(x => x.Id).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<int?>> Star(string id)
        {
            try
            {
                return await db.Comments.Where(x => x.ObjectId == id && x.ObjectType == "product" && x.ParentId == null).Select(x => x.Star).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProductCmtVM>> CmtChildren(string parentId)
        {
            try
            {
                return await (from cmt in db.Comments
                              join pic in db.Pictures on cmt.UserId equals pic.ObjectId into list
                              from pic in list.DefaultIfEmpty()
                              join u in db.Users on cmt.UserId equals u.Id
                              where cmt.ParentId == parentId
                              select new ProductCmtVM
                              {
                                  Id = cmt.Id,
                                  UserId = cmt.UserId,
                                  Content = cmt.Content,
                                  ObjectId = cmt.ObjectId,
                                  ObjectType = cmt.ObjectType,
                                  OrderDetailId = cmt.OrderDetailId,
                                  Star = cmt.Star,
                                  CreatedAt = cmt.CreatedAt,
                                  ImageName = pic.Name,
                                  ImageSrc = null,
                                  FullName = u.LastName + " " + u.FirstName,
                                  Children=null,
                              }).OrderByDescending(x => x.CreatedAt).ToListAsync();

            }
            catch
            {
                return null;
            }
        }
    }
}
