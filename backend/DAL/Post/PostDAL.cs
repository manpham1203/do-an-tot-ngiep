using BO;
using BO.ViewModels.Picture;
using BO.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Post
{
    public class PostDAL
    {
        private AppDbContext db;
        public PostDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await (from post in db.Posts where post.Id == id select new { post.Id }).ToListAsync();
                if (resultFromDb.Count == 0)
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
        public async Task<bool> Create(PostVM postVM, PictureVM pictureVM)
        {
            var post = new BO.Entities.Post
            {
                Id = postVM.Id,
                Title = postVM.Title,
                Slug = postVM.Slug,
                FullDescription = postVM.FullDescription,
                ShortDescription = postVM.ShortDescription,
                Published = postVM.Published,
                Deleted = postVM.Deleted,
                View = postVM.View,
                CreatedAt = postVM.CreatedAt,
                UpdatedAt = postVM.UpdatedAt,
            };
            await db.Posts.AddAsync(post);
            var resultPost = await db.SaveChangesAsync();
            if (resultPost == 0)
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
        public async Task<bool> Update(PostVM postVM, PictureVM pictureVM)
        {
            var postFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == postVM.Id);

            postFromDb.Title = postVM.Title;
            postFromDb.Slug = postVM.Slug;
            postFromDb.FullDescription = postVM.FullDescription;
            postFromDb.ShortDescription = postVM.ShortDescription;
            postFromDb.Published = postVM.Published;
            postFromDb.UpdatedAt = postVM.UpdatedAt;

            var resultPost = await db.SaveChangesAsync();
            if (resultPost == 0)
            {
                return false;
            }
            if (pictureVM.Id != null)
            {
                var pictureFromDb = await db.Pictures.SingleOrDefaultAsync(x => x.ObjectId == postFromDb.Id);
                pictureFromDb.Name = pictureVM.Name;

                var resultPicture = await db.SaveChangesAsync();
                if (resultPicture == 0)
                {
                    return false;
                }
            }



            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var postFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
            db.Posts.Remove(postFromDb);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return false;
            }
            var pictureFromDb = await db.Pictures.Where(x => x.ObjectId == id).Where(x => x.ObjectType == "post").SingleOrDefaultAsync();
            db.Pictures.Remove(pictureFromDb);
            var resultPicture = await db.SaveChangesAsync();
            if (resultPicture == 0)
            {
                return false;
            }
            return false;
        }

        public async Task<List<string>> RowsAdminDeleted(bool deleted, string query)
        {
            try
            {
                var resultFromDb = await db.Posts.Where(x => x.Deleted == deleted).Select(x => new { x.Id, x.Title }).ToListAsync();
                if (!string.IsNullOrEmpty(query))
                {
                    resultFromDb = resultFromDb.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList();
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<string>();
                }
                return resultFromDb.Select(x => x.Id).ToList();
            }
            catch
            {
                return null;
            }
        }
        public async Task<RowAdmin> RowsAdminById(string id)
        {
            try
            {
                //var resultFromDb = await db.Posts.Select(x => new { x.Id, x.Title, x.Published }).SingleOrDefaultAsync(x => x.Id == id);
                var resultFromDb = await (from post in db.Posts
                                          join pic in db.Pictures on post.Id equals pic.ObjectId into list
                                          from pic in list.DefaultIfEmpty()
                                          where pic.ObjectType == "post"
                                          select new RowAdmin
                                          {
                                              Id = post.Id,
                                              Title = post.Title,
                                              Published = post.Published,
                                              ImageName = pic.Name,
                                              ImageSrc = null,
                                          }).Where(x => x.Id == id).SingleOrDefaultAsync();
                //var result = new RowAdmin
                //{
                //    Id = resultFromDb.Id,
                //    Title = resultFromDb.Title,
                //    Published = resultFromDb.Published,
                //};
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Published(string id)
        {
            try
            {
                var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
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
                var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
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

        public async Task<SetDataUpdateVM> SetDataUpdate(string id)
        {
            try
            {
                var resultFromDb = await db.Posts.Select(x => new { x.Id, x.Title, x.ShortDescription, x.FullDescription, x.Published }).SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                return new SetDataUpdateVM
                {
                    Title = resultFromDb.Title,
                    ShortDescription = resultFromDb.ShortDescription,
                    FullDescription = resultFromDb.FullDescription,
                    Published = resultFromDb.Published,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteFromDatabase(string id)
        {
            try
            {
                var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                db.Posts.Remove(resultFromDb);
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                var pictureFromDb = await db.Pictures.Where(x => x.ObjectId == id).Where(x => x.ObjectType == "post").SingleOrDefaultAsync();
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

        public async Task<List<PostCardVM>> PostCards()
        {
            try
            {
                //var resultFromDb = await db.Posts.Where(x => x.Published==true && x.Deleted==false).Select(x => new { x.Id, x.Title,x.Slug, x.ShortDescription, x.View, x.Image, x.CreatedAt }).ToListAsync();
                var resultFromDb = await (from post in db.Posts
                                          join pic in db.Pictures on post.Id equals pic.ObjectId
                                          where pic.ObjectId == post.Id && pic.ObjectType == "post"
                                          select new PostCardVM
                                          {
                                              Id = post.Id,
                                              Title = post.Title,
                                              Slug = post.Slug,
                                              ShortDescription = post.ShortDescription,
                                              View = post.View,
                                              Image = pic.Name,
                                              CreatedAt = post.CreatedAt,
                                          }).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<PostCardVM>();
                }
                //var result = resultFromDb.Select(x => new PostCardVM
                //{
                //    Id = x.Id,
                //    Title = x.Title,
                //    Slug = x.Slug,
                //    ShortDescription = x.ShortDescription,
                //    View = x.View,
                //    Image = x.Image,
                //    CreatedAt = x.CreatedAt,
                //}).ToList();
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<PostDetailVM> PostDetail(string slug)
        {
            try
            {
                var resultFromDb = await (from post in db.Posts
                                          join pic in db.Pictures on post.Id equals pic.ObjectId
                                          where post.Slug == slug && pic.ObjectId == post.Id && pic.ObjectType == "post"
                                          select new PostDetailVM
                                          {
                                              Id = post.Id,
                                              Title = post.Title,
                                              Slug = post.Slug,
                                              ShortDescription = post.ShortDescription,
                                              FullDescription = post.FullDescription,
                                              View = post.View,
                                              Image = pic.Name,
                                              CreatedAt = post.CreatedAt,
                                          }).SingleOrDefaultAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                //var result = new PostDetailVM
                //{
                //    Title = resultFromDb.Title,
                //    Slug = resultFromDb.Slug,
                //    ShortDescription = resultFromDb.ShortDescription,
                //    FullDescription = resultFromDb.FullDescription,
                //    View = resultFromDb.View,
                //    Image = resultFromDb.Image,
                //    CreatedAt = resultFromDb.CreatedAt,
                //};
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }
        public async Task<PostDetailVM> PostDetailId(string id)
        {
            try
            {
                var resultFromDb = await (from post in db.Posts
                                          join pic in db.Pictures on post.Id equals pic.ObjectId
                                          where post.Id == id && pic.ObjectId == post.Id && pic.ObjectType == "post"
                                          select new PostDetailVM
                                          {
                                              Id = post.Id,
                                              Title = post.Title,
                                              Slug = post.Slug,
                                              ShortDescription = post.ShortDescription,
                                              FullDescription = post.FullDescription,
                                              View = post.View,
                                              Image = pic.Name,
                                              ImageSrc = null,
                                              Published = post.Published,
                                              CreatedAt = post.CreatedAt,
                                              UpdatedAt = post.UpdatedAt,
                                          }).SingleOrDefaultAsync();
                if (resultFromDb == null)
                {
                    return null;
                }

                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IncreaseView(string id)
        {
            try
            {
                var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.View += 1;
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

        public async Task<bool> PublishedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Published = true;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PublishedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Published = false;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Deleted = true;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Deleted = false;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
