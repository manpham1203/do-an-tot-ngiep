
using BO;
using BO.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public class ProductDAL
    {
        private AppDbContext db;
        public ProductDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<ProductVM>> GetAll()
        {
            try
            {
                var productFromDb = await db.Products.ToListAsync();
                if (productFromDb == null)
                {
                    return null;
                }
                var productVMs = productFromDb.Select(x => new ProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Price = x.Price,
                    PriceDiscount = x.PriceDiscount,
                    FullDescription = x.FullDescription,
                    ShortDescription = x.ShortDescription,
                    Quantity = x.Quantity,
                    IsActive = x.IsActive,
                    Deleted = x.Deleted,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    BrandId = x.BrandId
                }).ToList();
                return productVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductVM> GetById(string id)
        {
            try
            {
                var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
                if (productFromDb == null)
                {
                    return null;
                }
                var productVMs = new ProductVM
                {
                    Id = productFromDb.Id,
                    Name = productFromDb.Name,
                    Slug = productFromDb.Slug,
                    Price = productFromDb.Price,
                    PriceDiscount = productFromDb.PriceDiscount,
                    FullDescription = productFromDb.FullDescription,
                    ShortDescription = productFromDb.ShortDescription,
                    Quantity = productFromDb.Quantity,
                    IsActive = productFromDb.IsActive,
                    Deleted = productFromDb.Deleted,
                    CreatedAt = productFromDb.CreatedAt,
                    UpdatedAt = productFromDb.UpdatedAt,
                    BrandId = productFromDb.BrandId
                };
                return productVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {
            try
            {
                var productFromDb = await db.Products.ToListAsync();
                if (productFromDb == null)
                {
                    return null;
                }
                var productByBrand = productFromDb.Where(x => x.BrandId == id);
                if (productFromDb == null)
                {
                    return null;
                }
                var productVMs = productByBrand.Select(x => new ProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Price = x.Price,
                    PriceDiscount = x.PriceDiscount,
                    FullDescription = x.FullDescription,
                    ShortDescription = x.ShortDescription,
                    Quantity = x.Quantity,
                    IsActive = x.IsActive,
                    Deleted = x.Deleted,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    BrandId = x.BrandId
                }).ToList();
                return productVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(ProductVM productVM)
        {
            try
            {
                var product = new BO.Entities.Product
                {
                    Id = productVM.Id,
                    Name = productVM.Name,
                    Slug = productVM.Slug,
                    Price = productVM.Price,
                    PriceDiscount = productVM.PriceDiscount,
                    FullDescription = productVM.FullDescription,
                    ShortDescription = productVM.ShortDescription,
                    Quantity = productVM.Quantity,
                    IsActive = productVM.IsActive,
                    Deleted = productVM.Deleted,
                    CreatedAt = productVM.CreatedAt,
                    UpdatedAt = productVM.UpdatedAt,
                    BrandId = productVM.BrandId
                };
                await db.Products.AddAsync(product);
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
        public async Task<bool> Update(ProductVM productVM)
        {
            try
            {
                var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == productVM.Id);
                if (productFromDb == null)
                {
                    return false;
                }

                productFromDb.Name = productVM.Name;
                productFromDb.Slug = productVM.Slug;
                productFromDb.Price = productVM.Price;
                productFromDb.PriceDiscount = productVM.PriceDiscount;
                productFromDb.FullDescription = productVM.FullDescription;
                productFromDb.ShortDescription = productVM.ShortDescription;
                productFromDb.Quantity = productVM.Quantity;
                productFromDb.IsActive = productVM.IsActive;
                productFromDb.Deleted = productVM.Deleted;
                productFromDb.UpdatedAt = productVM.UpdatedAt;
                productFromDb.BrandId = productVM.BrandId;

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
                var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
                db.Products.Remove(productFromDb);
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
