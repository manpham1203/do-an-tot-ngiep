using BO;
using BO.ViewModels.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Comment
{
    public class ProductCmtDAL
    {
        private readonly AppDbContext db;
        public ProductCmtDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Comments.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                return true;

            }
            catch
            {
                return true;
            }
        }
        public async Task<List<ProductCmtVM>> ProductCmts(string productId)
        {
            try
            {
                var resultFromDb = await db.Comments.Where(x => x.ObjectId == productId).Where(x => x.ObjectType == "product").ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductCmtVM>();
                }
                var result = resultFromDb.Select(x => new ProductCmtVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Content = x.Content,
                    Star = x.Star,
                    ObjectId = x.ObjectId,
                    ObjectType = x.ObjectType,
                    OrderDetailId = x.OrderDetailId,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(ProductCmtVM model)
        {
            try
            {
                var obj = new BO.Entities.Comment
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Content = model.Content,
                    Star = model.Star,
                    ObjectId = model.ObjectId,
                    ObjectType = model.ObjectType,
                    OrderDetailId = model.OrderDetailId,
                    CreatedAt=model.CreatedAt,
                };
                await db.Comments.AddAsync(obj);
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
