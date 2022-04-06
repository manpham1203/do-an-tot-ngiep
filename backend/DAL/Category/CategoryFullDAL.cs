using BO;
using BO.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Category
{
    public class CategoryFullDAL
    {
        private AppDbContext db;

        public CategoryFullDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<CategoryFullVM>> GetAll()
        {
            var categoryFromDb = await db.Categories.ToListAsync();
            if (categoryFromDb.Count==0)
            {
                return new List<CategoryFullVM>();
            }
            var categoryVMs = categoryFromDb.Select(x => new CategoryFullVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
                ProductVMs = null
            }).ToList();
            return categoryVMs;
        }
        public async Task<CategoryFullVM> GetById(string id)
        {
            var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (categoryFromDb == null)
            {
                return null;
            }
            var categoryVM = new CategoryFullVM
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name,
                Slug = categoryFromDb.Slug,
                FullDescription = categoryFromDb.FullDescription,
                ShortDescription = categoryFromDb.ShortDescription,
                Pulished = categoryFromDb.Pulished,
                Deleted = categoryFromDb.Deleted,
                CreatedAt = categoryFromDb.CreatedAt,
                UpdatedAt = categoryFromDb.UpdatedAt,
                Ordinal = categoryFromDb.Ordinal,
                ProductVMs = null
            };
            return categoryVM;
        }
        public async Task<CategoryFullVM> GetBySlug(string slug)
        {
            var resultFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Slug == slug);
            if (resultFromDb == null)
            {
                return null;
            }
            var brandFullVM = new CategoryFullVM
            {
                Id = resultFromDb.Id,
                Name = resultFromDb.Name,
                Slug = resultFromDb.Slug,
                FullDescription = resultFromDb.FullDescription,
                ShortDescription = resultFromDb.ShortDescription,
                Pulished = resultFromDb.Pulished,
                Deleted = resultFromDb.Deleted,
                CreatedAt = resultFromDb.CreatedAt,
                UpdatedAt = resultFromDb.UpdatedAt,
                Ordinal = resultFromDb.Ordinal,
                ProductVMs = null,
            };
            return brandFullVM;
        }
    }
}
