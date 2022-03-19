
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
    public class CategoryDAL
    {
        private readonly AppDbContext db;
        public CategoryDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            try
            {
                var categoryFromDb = await db.Categories.ToListAsync();
                if (categoryFromDb == null)
                {
                    return null;
                }
                var categoryVMs = categoryFromDb.Select(x => new CategoryVM
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
                }).ToList();
                return categoryVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CategoryVM> GetById(string id)
        {
            try
            {
                var categoryFromDb = await db.Categories.SingleOrDefaultAsync(c => c.Id == id);
                if (categoryFromDb == null)
                {
                    return null;
                }
                var categoryVM = new CategoryVM();

                categoryVM.Id = categoryFromDb.Id;
                categoryVM.Name = categoryFromDb.Name;
                categoryVM.Slug = categoryFromDb.Slug;
                categoryVM.FullDescription = categoryFromDb.FullDescription;
                categoryVM.ShortDescription = categoryFromDb.ShortDescription;
                categoryVM.IsActive = categoryFromDb.IsActive;
                categoryVM.Deleted = categoryFromDb.Deleted;
                categoryVM.CreatedAt = categoryFromDb.CreatedAt;
                categoryVM.UpdatedAt = categoryFromDb.UpdatedAt;
                categoryVM.Ordinal = categoryFromDb.Ordinal;

                return categoryVM;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(CategoryVM categoryVM)
        {
            try
            {
                var category = new BO.Entities.Category
                {
                    Id = categoryVM.Id,
                    Name = categoryVM.Name,
                    Slug = categoryVM.Slug,
                    FullDescription = categoryVM.FullDescription,
                    ShortDescription = categoryVM.ShortDescription,
                    IsActive = categoryVM.IsActive,
                    Deleted = categoryVM.Deleted,
                    CreatedAt = categoryVM.CreatedAt,
                    UpdatedAt = categoryVM.UpdatedAt,
                };
                await db.Categories.AddAsync(category);
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
        public async Task<bool> Update(CategoryVM categoryVM)
        {
            try
            {
                var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == categoryVM.Id);
                if (categoryFromDb == null)
                {
                    return false;
                }
                categoryFromDb.Name = categoryVM.Name;
                categoryFromDb.Slug = categoryVM.Slug;
                categoryFromDb.FullDescription = categoryVM.FullDescription;
                categoryFromDb.ShortDescription = categoryVM.ShortDescription;
                categoryFromDb.IsActive = categoryVM.IsActive;
                categoryFromDb.Deleted = categoryVM.Deleted;
                categoryFromDb.UpdatedAt = categoryVM.UpdatedAt;
                categoryFromDb.Ordinal = categoryVM.Ordinal;
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
        public async Task<bool> Delete(string id)
        {
            try
            {
                var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);
                db.Categories.Remove(categoryFromDb);
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
    }
}
