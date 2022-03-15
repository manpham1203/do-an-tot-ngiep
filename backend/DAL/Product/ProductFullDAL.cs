using BOL;
using BOL.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace DAL.Product
{
    public class ProductFullDAL
    {
        private WebDbContext db;
        //private Mapper mapper;
        public ProductFullDAL()
        {
            db = new WebDbContext();

            //var configMapper = new MapperConfiguration(config =>
            //  {
            //      config.CreateMap<BOL.Entities.Product, ProductFullVM>().ReverseMap();
            //  });
            //mapper=new Mapper(configMapper);
        }
        public async Task<List<ProductFullVM>> GetAll()
        {
            try
            {
                var productFromDb = await db.Products.ToListAsync();
                if (productFromDb == null)
                {
                    return null;
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
                    IsActive = x.IsActive,
                    Deleted = x.Deleted,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    BrandId = x.BrandId,
                    CategoryVMs = null,
                }).ToList();
                return productVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductFullVM> GetById(string id)
        {
            try
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
                    IsActive = productFromDb.IsActive,
                    Deleted = productFromDb.Deleted,
                    CreatedAt = productFromDb.CreatedAt,
                    UpdatedAt = productFromDb.UpdatedAt,
                    BrandId = productFromDb.BrandId,
                    BrandVM=null,
                    CategoryVMs=null,
                };
                return productFullVMs;
            }
            catch
            {
                return null;
            }
        }
    }
}
