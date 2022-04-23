using BO;
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
        public async Task<bool> Create(PostVM postVM)
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
                Likes = postVM.Likes,
                Views = postVM.Views,
                Image = postVM.Image,
                CreatedAt = postVM.CreatedAt,
                UpdatedAt = postVM.UpdatedAt,
            };
            await db.Posts.AddAsync(post);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(PostVM postVM)
        {
            var postFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == postVM.Id);

            postFromDb.Title = postVM.Title;
            postFromDb.Slug = postVM.Slug;
            postFromDb.FullDescription = postVM.FullDescription;
            postFromDb.ShortDescription = postVM.ShortDescription;
            postFromDb.Published = postVM.Published;
            postFromDb.UpdatedAt = postVM.UpdatedAt;
            if (!string.IsNullOrEmpty(postVM.Image))
            {
                postFromDb.Image = postVM.Image;
            }

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var postFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
            db.Posts.Remove(postFromDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
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
                var resultFromDb = await db.Posts.Select(x => new { x.Id, x.Title, x.Published }).SingleOrDefaultAsync(x => x.Id == id);

                var result = new RowAdmin
                {
                    Id = resultFromDb.Id,
                    Title = resultFromDb.Title,
                    Published = resultFromDb.Published,
                };
                return result;
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
                var resultFromDb = await db.Posts.Select(x => new { x.Id, x.Title, x.ShortDescription, x.FullDescription, x.Published, x.Image }).SingleOrDefaultAsync(x => x.Id == id);
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
                    Image = resultFromDb.Image
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
    
        public async Task<List<PostCardVM>> PostCards()
        {
            try
            {
                var resultFromDb = await db.Posts.Where(x => x.Published==true && x.Deleted==false).Select(x => new { x.Id, x.Title,x.Slug, x.ShortDescription, x.Views, x.Image, x.CreatedAt }).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<PostCardVM>();
                }
                var result = resultFromDb.Select(x => new PostCardVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    Views = x.Views,
                    Image = x.Image,
                    CreatedAt = x.CreatedAt,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
