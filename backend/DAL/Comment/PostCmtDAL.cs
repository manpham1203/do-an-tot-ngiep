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
    public class PostCmtDAL
    {
        private readonly AppDbContext db;
        public PostCmtDAL()
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

        public async Task<bool> Create(PostCmtVM model)
        {
            try
            {
                var obj = new BO.Entities.Comment
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Content = model.Content,
                    Star = null,
                    ObjectId = model.ObjectId,
                    ObjectType = model.ObjectType,
                    OrderDetailId = null,
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
        public async Task<PostCmtVM> CommentItem(string id)
        {
            try
            {
                var resultFromDb = await (from cmt in db.Comments
                                          join pic in db.Pictures on cmt.UserId equals pic.ObjectId into list
                                          from pic in list.DefaultIfEmpty()
                                          join u in db.Users on cmt.UserId equals u.Id
                                          where cmt.Id == id
                                          select new PostCmtVM
                                          {
                                              Id = cmt.Id,
                                              UserId = cmt.UserId,
                                              Content = cmt.Content,
                                              ObjectId = cmt.ObjectId,
                                              ObjectType = cmt.ObjectType,
                                              CreatedAt = cmt.CreatedAt,
                                              ImageName = pic.Name,
                                              ImageSrc = null,
                                              FullName = u.LastName + " " + u.FirstName,
                                              Children = null,
                                          }).SingleOrDefaultAsync();
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<string>> IdsOfPost(string postId)
        {
            try
            {
                var resultFromDb = await db.Comments.OrderByDescending(x => x.CreatedAt).Where(x => x.ObjectId == postId && x.ObjectType == "post" && x.ParentId == null).ToListAsync();
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

        public async Task<List<PostCmtVM>> CmtChildren(string parentId)
        {
            try
            {
                return await (from cmt in db.Comments
                              join pic in db.Pictures on cmt.UserId equals pic.ObjectId into list
                              from pic in list.DefaultIfEmpty()
                              join u in db.Users on cmt.UserId equals u.Id
                              where cmt.ParentId == parentId
                              select new PostCmtVM
                              {
                                  Id = cmt.Id,
                                  UserId = cmt.UserId,
                                  Content = cmt.Content,
                                  ObjectId = cmt.ObjectId,
                                  ObjectType = cmt.ObjectType,
                                  CreatedAt = cmt.CreatedAt,
                                  ImageName = pic.Name,
                                  ImageSrc = null,
                                  FullName = u.LastName + " " + u.FirstName,
                                  Children = null,
                              }).OrderByDescending(x => x.CreatedAt).ToListAsync();

            }
            catch
            {
                return null;
            }
        }
    }
}
