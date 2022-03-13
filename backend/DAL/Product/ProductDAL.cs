using BOL;
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
        private readonly WebDbContext db;
        public ProductDAL()
        {
            db = new WebDbContext();
        }
        public async Task<IEnumerable<ProductDAL>> GetAll()
        {
            try
            {
                var productFromDb = await db.Products.ToListAsync();
                if(productFromDb == null)
                {
                    return null;
                }
                return 
            }
            catch
            {

            }
        }
    }
}
