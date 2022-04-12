using BO;
using BO.ViewModels.Brand;
using BO.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Brand
{
    public class BrandDAL
    {
        private readonly AppDbContext db;
        public BrandDAL()
        {
            db = new AppDbContext();
        }
        //public BrandDAL() { }
        //public BrandDAL(AppDbContext context)
        //{
        //    db= context;
        //}
        public async Task<List<BrandVM>> GetAll()
        {
            var brandFromDb = await db.Brands.ToListAsync();

            if (brandFromDb.Count == 0)
            {
                return new List<BrandVM>();
            }

            var brandVMs = brandFromDb.Select(x => new BrandVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return brandVMs;

        }
        public async Task<BrandVM> GetById(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(b => b.Id == id);
            if (brandFromDb == null)
            {
                return null;
            }
            var brandVM = new BrandVM();

            brandVM.Id = brandFromDb.Id;
            brandVM.Name = brandFromDb.Name;
            brandVM.Slug = brandFromDb.Slug;
            brandVM.FullDescription = brandFromDb.FullDescription;
            brandVM.ShortDescription = brandFromDb.ShortDescription;
            brandVM.Published = brandFromDb.Published;
            brandVM.Deleted = brandFromDb.Deleted;
            brandVM.CreatedAt = brandFromDb.CreatedAt;
            brandVM.UpdatedAt = brandFromDb.UpdatedAt;
            brandVM.Ordinal = brandFromDb.Ordinal;

            return brandVM;
        }
        public async Task<bool> Create(BrandVM brandVM)
        {
            var brand = new BO.Entities.Brand
            {
                Id = brandVM.Id,
                Name = brandVM.Name,
                Slug = brandVM.Slug,
                FullDescription = brandVM.FullDescription,
                ShortDescription = brandVM.ShortDescription,
                Published = brandVM.Published,
                Deleted = brandVM.Deleted,
                CreatedAt = brandVM.CreatedAt,
                UpdatedAt = brandVM.UpdatedAt,
            };
            await db.Brands.AddAsync(brand);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(BrandVM brandVM)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == brandVM.Id);

            brandFromDb.Name = brandVM.Name;
            brandFromDb.Slug = brandVM.Slug;
            brandFromDb.FullDescription = brandVM.FullDescription;
            brandFromDb.ShortDescription = brandVM.ShortDescription;
            brandFromDb.Published = brandVM.Published;
            brandFromDb.Deleted = brandVM.Deleted;
            brandFromDb.UpdatedAt = brandVM.UpdatedAt;
            brandFromDb.Ordinal = brandVM.Ordinal;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
            db.Brands.Remove(brandFromDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool published)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Published = published;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id, bool deleted)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Deleted = deleted;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<BrandVM>> GetAllBrandDeleted(bool deleted)
        {
            var brandFromDb = await db.Brands.Where(x => x.Deleted == deleted).ToListAsync();

            if (brandFromDb.Count == 0)
            {
                return new List<BrandVM>();
            }

            var brandVMs = brandFromDb.Select(x => new BrandVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return brandVMs;

        }

        public async Task<List<BrandNameVM>> AllBrandName()
        {
            try
            {
                var resultFromDb = await db.Brands.Where(x => x.Published == true && x.Deleted == false).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<BrandNameVM>();
                }
                var listBrandNameVM = resultFromDb.Select(x => new BrandNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug=x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return listBrandNameVM;
            }
            catch
            {
                return null;
            }

        }
       
        public async Task<List<BrandNameVM>> AllBrandName(bool deleted)
        {
            try
            {
                var resultFromDb = await db.Brands.Where(x => x.Deleted == deleted).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<BrandNameVM>();
                }
                var listBrandNameVM = resultFromDb.Select(x => new BrandNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return listBrandNameVM;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BrandRowAdminVM> BrandRowAmin(string id)
        {
            try
            {
                var resultFromDb = await db.Brands.SingleOrDefaultAsync(x =>x.Id==id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new BrandRowAdminVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    CreatedAt = resultFromDb.CreatedAt,
                    Ordinal = resultFromDb.Ordinal,
                };
                return result;
            }
            catch
            {
                return null;
            }

        }
    }
}
