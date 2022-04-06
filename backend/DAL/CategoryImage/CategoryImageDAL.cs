using BO;
using BO.ViewModels.CategoryImage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CategoryImage
{
    public class CategoryImageDAL
    {
        private readonly AppDbContext db;
        public CategoryImageDAL()
        {
            db = new AppDbContext();
        }
        public async Task<CategoryImageVM> GetById(string id)
        {
            try
            {
                var imgFromDb = await db.CategoryImages.SingleOrDefaultAsync(b => b.Id == id);
                if (imgFromDb == null)
                {
                    return null;
                }
                var brandImageVM = new CategoryImageVM();

                brandImageVM.Id = imgFromDb.Id;
                brandImageVM.Name = imgFromDb.Name;
                brandImageVM.CategoryId = imgFromDb.CategoryId;
                brandImageVM.Pulished = imgFromDb.Pulished;

                return brandImageVM;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<CategoryImageVM>> GetByCategoryId(string id)
        {
            var imgFromDb = await db.CategoryImages.Where(b => b.CategoryId == id).ToListAsync();
            if (imgFromDb == null)
            {
                return new List<CategoryImageVM>();
            }
            var categoryImgs = imgFromDb.Select(x => new CategoryImageVM
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Pulished = x.Pulished,
            }).ToList();

            return categoryImgs;
        }

        public async Task<bool> Create(List<CategoryImageVM> obj)
        {
            var imgs = obj.Select(x => new BO.Entities.CategoryImage
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Pulished = x.Pulished,
            });
            foreach (var img in imgs)
            {
                await db.CategoryImages.AddAsync(img);
            }
            var result = await db.SaveChangesAsync();
            if (result >= imgs.Count())
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var imgFormDb = await db.CategoryImages.SingleOrDefaultAsync(x => x.Id == id);
            if (imgFormDb == null)
            {
                return false;
            }
            db.CategoryImages.Remove(imgFormDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool pulished)
        {
            var categoryImage = await db.CategoryImages.SingleOrDefaultAsync(x => x.Id == id);

            categoryImage.Pulished = pulished;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

    }
}
