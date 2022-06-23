
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BO;
using BO.ViewModels.Product;

namespace DAL.Product
{
    public class ProductFullDAL
    {
        private AppDbContext db;
        //private Mapper mapper;
        public ProductFullDAL()
        {
            db = new AppDbContext();

            //var configMapper = new MapperConfiguration(config =>
            //  {
            //      config.CreateMap<BOL.Entities.Product, ProductFullVM>().ReverseMap();
            //  });
            //mapper=new Mapper(configMapper);
        }
        public async Task<List<ProductFullVM>> GetAll()
        {

            var productFromDb = await db.Products.ToListAsync();
            if (productFromDb.Count==0)
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
                Description = x.Description,
                QuantityInStock = x.QuantityInStock,
                Published = x.Published,
                Deleted = x.Deleted,
                View = x.View,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId,
                CategoryVMs = null,
            }).ToList();
            return productVMs;

        }
        public async Task<ProductFullVM> GetById(string id)
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
                Description = productFromDb.Description,
                QuantityInStock = productFromDb.QuantityInStock,
                Published = productFromDb.Published,
                Deleted = productFromDb.Deleted,
                View = productFromDb.View,
                CreatedAt = productFromDb.CreatedAt,
                UpdatedAt = productFromDb.UpdatedAt,
                BrandId = productFromDb.BrandId,
                BrandVM = null,
                CategoryVMs = null,
            };
            return productFullVMs;

        }
        public async Task<ProductFullVM> GetBySlug(string slug)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Slug == slug);
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
                Description = productFromDb.Description,
                QuantityInStock = productFromDb.QuantityInStock,
                Published = productFromDb.Published,
                Deleted = productFromDb.Deleted,
                View = productFromDb.View,
                CreatedAt = productFromDb.CreatedAt,
                UpdatedAt = productFromDb.UpdatedAt,
                BrandId = productFromDb.BrandId,
                BrandVM = null,
                CategoryVMs = null,
            };
            return productFullVMs;

        }
        public async Task<List<ProductFullVM>> GetByBrandId(string id)
        {

            var productFromDb = await db.Products.Where(x => x.BrandId == id).ToListAsync();
            if (productFromDb == null)
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
                Description = x.Description,
                QuantityInStock = x.QuantityInStock,
                Published = x.Published,
                Deleted = x.Deleted,
                View = x.View,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
    }
}
