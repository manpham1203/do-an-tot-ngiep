using BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Wishlist
{
    public class WishlistDAL
    {
        private readonly AppDbContext db;
        public WishlistDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string userId, string productId)
        {
            try
            {
                var resultFromDb = await db.Wishlists.Where(x => x.UserId == userId && x.ProductId == productId).SingleOrDefaultAsync();
                if (resultFromDb != null)
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
        public async Task<bool> Create(string userId, string productId)
        {
            try
            {
                var obj = new BO.Entities.Wishlist
                {
                    UserId = userId,
                    ProductId = productId,
                };
                await db.Wishlists.AddAsync(obj);
                var result=await db.SaveChangesAsync();
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
        public async Task<bool> Delete(string userId, string productId)
        {
            try
            {
                //var obj = new BO.Entities.Wishlist
                //{
                //    UserId = userId,
                //    ProductId = productId,
                //};
                var resultFromDb = await db.Wishlists.Where(x => x.ProductId == productId && x.UserId == userId).SingleOrDefaultAsync();
                db.Wishlists.Remove(resultFromDb);
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
    
        public async Task<int> Count(string productId)
        {
            try
            {
                return await db.Wishlists.CountAsync(x => x.ProductId == productId);                
            }
            catch
            {
                return 0;
            }
        }
    }
}
