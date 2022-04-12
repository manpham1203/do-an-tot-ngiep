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
        public async Task<List<PostVM>> GetAll()
        {
            var postFromDb = await db.Posts.ToListAsync();
            if (postFromDb.Count==0)
            {
                return new List<PostVM>();
            }
            var postVMs = postFromDb.Select(x => new PostVM
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
            }).ToList();
            return postVMs;

        }
        public async Task<PostVM> GetById(string id)
        {

            var postFromDb = await db.Posts.SingleOrDefaultAsync(x => x.Id == id);
            if (postFromDb == null)
            {
                return null;
            }
            var postVM = new PostVM
            {
                Id = postFromDb.Id,
                Title = postFromDb.Title,
                Slug = postFromDb.Slug,
                FullDescription = postFromDb.FullDescription,
                ShortDescription = postFromDb.ShortDescription,
                Published = postFromDb.Published,
                Deleted = postFromDb.Deleted,
                Likes = postFromDb.Likes,
                Views = postFromDb.Views,
                CreatedAt = postFromDb.CreatedAt,
                UpdatedAt = postFromDb.UpdatedAt,
            };
            return postVM;

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
                CreatedAt = postVM.CreatedAt,
                UpdatedAt = postVM.UpdatedAt,
            };
            await db.AddAsync(post);
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
            postFromDb.Deleted = postVM.Deleted;
            postFromDb.Likes = postVM.Likes;
            postFromDb.Views = postVM.Views;
            postFromDb.UpdatedAt = postVM.UpdatedAt;

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
    }
}
