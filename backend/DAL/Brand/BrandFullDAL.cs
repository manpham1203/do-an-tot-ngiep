using BOL;
using BOL.ViewModels.Brand;
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
        private readonly WebDbContext db;
        public BrandFullDAL()
        {
            db=new WebDbContext();
        }
        public async Task<List<BrandFullVM>> GetAll()
        {
            try
            {
                var brandFromDb = await db.Brands.ToListAsync();
                if (brandFromDb == null)
                {
                    return null;
                }
                var brandFullVMs = brandFromDb.Select(x => new BrandFullVM
                {
                    Id=x.Id,
                    Name=x.Name,
                    Slug=x.Slug,
                    FullDescription=x.FullDescription,
                    ShortDescription=x.FullDescription,
                    IsActive=x.IsActive,
                    Deleted=x.Deleted,
                    CreatedAt=x.CreatedAt,
                    UpdatedAt=x.UpdatedAt,
                    Ordinal=x.Ordinal,
                    ProductVMs=null,
                }).ToList();
                return brandFullVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<BrandFullVM> GetById(string id)
        {
            try
            {
                var brandFromDb = await db.Brands.SingleOrDefaultAsync(x=>x.Id==id);
                if (brandFromDb == null)
                {
                    return null;
                }
                var brandFullVM =  new BrandFullVM
                {
                    Id = brandFromDb.Id,
                    Name = brandFromDb.Name,
                    Slug = brandFromDb.Slug,
                    FullDescription = brandFromDb.FullDescription,
                    ShortDescription = brandFromDb.FullDescription,
                    IsActive = brandFromDb.IsActive,
                    Deleted = brandFromDb.Deleted,
                    CreatedAt = brandFromDb.CreatedAt,
                    UpdatedAt = brandFromDb.UpdatedAt,
                    Ordinal = brandFromDb.Ordinal,
                    ProductVMs = null,
                };
                return brandFullVM;
            }
            catch
            {
                return null;
            }
        }
    }
}
