using BO;
using BO.ViewModels.Brand;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Brand
{
    public class BrandFullDAL
    {
        private readonly AppDbContext db;
        public BrandFullDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<BrandFullVM>> GetAll()
        {

            var brandFromDb = await db.Brands.ToListAsync();
            if (brandFromDb.Count==0)
            {
                return new List<BrandFullVM>();
            }
            var brandFullVMs = brandFromDb.Select(x => new BrandFullVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.FullDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                ProductVMs = null,
            }).ToList();
            return brandFullVMs;
        }
        public async Task<BrandFullVM> GetById(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
            if (brandFromDb == null)
            {
                return null;
            }
            var brandFullVM = new BrandFullVM
            {
                Id = brandFromDb.Id,
                Name = brandFromDb.Name,
                Slug = brandFromDb.Slug,
                FullDescription = brandFromDb.FullDescription,
                ShortDescription = brandFromDb.FullDescription,
                Published = brandFromDb.Published,
                Deleted = brandFromDb.Deleted,
                CreatedAt = brandFromDb.CreatedAt,
                UpdatedAt = brandFromDb.UpdatedAt,
                ProductVMs = null,
            };
            return brandFullVM;
        }
        public async Task<BrandFullVM> GetBySlug(string slug)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Slug == slug);
            if (brandFromDb == null)
            {
                return null;
            }
            var brandFullVM = new BrandFullVM
            {
                Id = brandFromDb.Id,
                Name = brandFromDb.Name,
                Slug = brandFromDb.Slug,
                FullDescription = brandFromDb.FullDescription,
                ShortDescription = brandFromDb.ShortDescription,
                Published = brandFromDb.Published,
                Deleted = brandFromDb.Deleted,
                CreatedAt = brandFromDb.CreatedAt,
                UpdatedAt = brandFromDb.UpdatedAt,
                ProductVMs = null,
            };
            return brandFullVM;
        }
    }
}
