
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

            var productFromDb = await db.Products.ToListAsync();
            if (productFromDb.Count==0)
            {
                return new List<ProductVM>();
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
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
        public async Task<ProductVM> GetById(string id)
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
                Pulished = productFromDb.Pulished,
                Deleted = productFromDb.Deleted,
                Likes = productFromDb.Likes,
                Views = productFromDb.Views,
                CreatedAt = productFromDb.CreatedAt,
                UpdatedAt = productFromDb.UpdatedAt,
                BrandId = productFromDb.BrandId
            };
            return productVMs;

        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {

            var productFromDb = await db.Products.Where(x => x.BrandId == id).ToListAsync();
            if (productFromDb == null)
            {
                return new List<ProductVM>();
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
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
        public async Task<bool> Create(ProductVM productVM)
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
                Pulished = productVM.Pulished,
                Deleted = productVM.Deleted,
                Likes = productVM.Likes,
                Views = productVM.Views,
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
        public async Task<bool> Update(ProductVM productVM)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == productVM.Id);

            productFromDb.Name = productVM.Name;
            productFromDb.Slug = productVM.Slug;
            productFromDb.Price = productVM.Price;
            productFromDb.PriceDiscount = productVM.PriceDiscount;
            productFromDb.FullDescription = productVM.FullDescription;
            productFromDb.ShortDescription = productVM.ShortDescription;
            productFromDb.Quantity = productVM.Quantity;
            productFromDb.Pulished = productVM.Pulished;
            productFromDb.Deleted = productVM.Deleted;
            productFromDb.Likes = productVM.Likes;
            productFromDb.Views = productVM.Views;
            productFromDb.UpdatedAt = productVM.UpdatedAt;
            productFromDb.BrandId = productVM.BrandId;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> Delete(string id)
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

        public async Task<List<ProductFullVM>> GetProductFullAll()
        {

            var productFromDb = await db.Products.ToListAsync();
            if (productFromDb.Count == 0)
            {
                return new List<ProductFullVM>();
            }
            var productVMs = productFromDb.Select(x => new ProductFullVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Price = x.Price,
                PriceDiscount = x.PriceDiscount,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Quantity = x.Quantity,
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId,
                CategoryVMs = null,
            }).ToList();
            return productVMs;

        }
        public async Task<ProductFullVM> GetProductFullById(string id)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (productFromDb == null)
            {
                return null;
            }
            var productFullVMs = new ProductFullVM
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Slug = productFromDb.Slug,
                Price = productFromDb.Price,
                PriceDiscount = productFromDb.PriceDiscount,
                FullDescription = productFromDb.FullDescription,
                ShortDescription = productFromDb.ShortDescription,
                Quantity = productFromDb.Quantity,
                Pulished = productFromDb.Pulished,
                Deleted = productFromDb.Deleted,
                Likes = productFromDb.Likes,
                Views = productFromDb.Views,
                CreatedAt = productFromDb.CreatedAt,
                UpdatedAt = productFromDb.UpdatedAt,
                BrandId = productFromDb.BrandId,
                BrandVM = null,
                CategoryVMs = null,
            };
            return productFullVMs;

        }
    }
}
