using BOL;
using BOL.ViewModels.ProductCategoryMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ProductCategoryMapping
{
    public class ProductCategoryMappingDAL
    {
        private readonly WebDbContext db;
        public ProductCategoryMappingDAL()
        {
            db = new WebDbContext();
        }
        public async Task<List<ProductCategoryMappingVM>> GetAll()
        {
            try
            {
                var resultFromDb = await db.Product_Category_Mappings.ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                var categoryVM_ProductVM = resultFromDb.Select(x => new ProductCategoryMappingVM
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,

                }).ToList();
                return categoryVM_ProductVM;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductCategoryMappingVM>> GetById(string id, string type)
        {
            try
            {
                var resultFromDb = await db.Product_Category_Mappings.ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                var temp = resultFromDb.Select(x => new ProductCategoryMappingVM
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,

                });
                var result = new List<ProductCategoryMappingVM>();
                switch (type)
                {
                    case "ProductId":
                        result = temp.Where(p => p.ProductId == id).ToList();
                        break;
                    case "CategoryId":
                        result = temp.Where(p => p.CategoryId == id).ToList();
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(ProductCategoryMappingVM model)
        {
            try
            {
                var ProductCategory = new BOL.Entities.Product_Category_Mapping
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
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string id, string type)
        {
            try
            {
                var temp = await db.Product_Category_Mappings.ToListAsync();
                switch (type)
                {
                    case "ProductId":
                        var productFromDb = temp.Where(x => x.ProductId == id).ToList();
                        foreach (var item in productFromDb)
                        {
                            db.Product_Category_Mappings.Remove(item);
                        }
                        break;
                    case "CategoryId":
                        var categoryFromDb = temp.Where(x => x.CategoryId == id).ToList();
                        foreach (var item in categoryFromDb)
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
            catch
            {
                return false;

            }
        }
    }
}
