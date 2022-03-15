using BOL;
using BOL.ViewModels.Category;
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
        private WebDbContext db;

        public CategoryFullDAL()
        {
            db = new WebDbContext();
        }
        public async Task<List<CategoryFullVM>> GetAll()
        {
            try
            {
                var categoryFromDb = await db.Categories.ToListAsync();
                if (categoryFromDb == null)
                {
                    return null;
                }
                var categoryVMs = categoryFromDb.Select(x => new CategoryFullVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    FullDescription = x.FullDescription,
                    ShortDescription = x.ShortDescription,
                    IsActive = x.IsActive,
                    Deleted = x.Deleted,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Ordinal = x.Ordinal,
                    ProductVMs = null
                }).ToList();
                return categoryVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CategoryFullVM> GetById(string id)
        {
            try
            {
                var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x=>x.Id==id);
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
                    IsActive = categoryFromDb.IsActive,
                    Deleted = categoryFromDb.Deleted,
                    CreatedAt = categoryFromDb.CreatedAt,
                    UpdatedAt = categoryFromDb.UpdatedAt,
                    Ordinal = categoryFromDb.Ordinal,
                    ProductVMs = null
                };
                return categoryVM;
            }
            catch
            {
                return null;
            }
        }
    }
}
