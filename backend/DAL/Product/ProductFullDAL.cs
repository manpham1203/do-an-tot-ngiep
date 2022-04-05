
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
