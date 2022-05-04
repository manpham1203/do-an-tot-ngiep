using DAL.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Wishlist
{
    public class WishlistBLL
    {
        private WishlistDAL wishlistDAL;
        public WishlistBLL()
        {
            wishlistDAL = new WishlistDAL();
        }
        public async Task<bool> CheckExists(string userId, string productId)
        {
            try
            {
                return await wishlistDAL.CheckExists(userId, productId);
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
                var checkExists = await CheckExists(userId, productId);
                if (!checkExists)
                {
                    return await wishlistDAL.Create(userId, productId);
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
                var checkExists = await CheckExists(userId, productId);
                if (checkExists)
                {
                    return await wishlistDAL.Delete(userId, productId);
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
                return await wishlistDAL.Count(productId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
