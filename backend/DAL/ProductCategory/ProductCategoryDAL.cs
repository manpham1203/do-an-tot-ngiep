
using BO;
using BO.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ProductCategory
{
    public class ProductCategoryDAL
    {
        private readonly AppDbContext db;
        public ProductCategoryDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<ProductCategoryVM>> GetAll()
        {

            var resultFromDb = await db.Product_Category_Mappings.ToListAsync();
            if (resultFromDb.Count == 0)
            {
                return new List<ProductCategoryVM>();
            }
            var modelVM = resultFromDb.Select(x => new ProductCategoryVM
            {
                ProductId = x.ProductId,
                CategoryId = x.CategoryId,

            }).ToList();
            return modelVM;

        }
        public async Task<List<ProductCategoryVM>> GetById(string id, string type)
        {
            var temp = new List<BO.Entities.ProductCategory>();
            switch (type)
            {
                case "ProductId":
                    temp = await db.Product_Category_Mappings.Where(p => p.ProductId == id).ToListAsync();
                    break;
                case "CategoryId":
                    temp = await db.Product_Category_Mappings.Where(p => p.CategoryId == id).ToListAsync(); ;
                    break;
                default:
                    break;
            }
            var objVM = temp.Select(x => new ProductCategoryVM
            {
                ProductId = x.ProductId,
                CategoryId = x.CategoryId,
            }).ToList();
            return objVM;
        }
        
        public async Task<bool> Create(ProductCategoryVM model)
        {

            var ProductCategory = new BO.Entities.ProductCategory
            {
                ProductId = model.ProductId,
                CategoryId = model.CategoryId,
            };
            await db.Product_Category_Mappings.AddAsync(ProductCategory);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> Delete(string id, string type)
        {

            var temp = new List<BO.Entities.ProductCategory>();
            switch (type)
            {
                case "ProductId":
                    temp = await db.Product_Category_Mappings.Where(p => p.ProductId == id).ToListAsync();
                    foreach (var item in temp)
                    {
                        db.Product_Category_Mappings.Remove(item);
                    }
                    break;
                case "CategoryId":
                    temp = await db.Product_Category_Mappings.Where(p => p.CategoryId == id).ToListAsync();
                    foreach (var item in temp)
                    {
                        db.Product_Category_Mappings.Remove(item);
                    }
                    break;
                default:
                    break;
            }
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
    }
}
