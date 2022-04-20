using BO;
using BO.ViewModels.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.OrderDetail
{
    public class OrderDetailDAL
    {
        private AppDbContext db;
        public OrderDetailDAL()
        {
            db= new AppDbContext();
        }
        public async Task<bool> Create(OrderDetailVM model)
        {
            try
            {
                var obj = new BO.Entities.OrderDetail
                {
                    Id = model.Id,
                    OrderId = model.OrderId,
                    ProductId=model.ProductId,
                    Quantity=model.Quantity,
                    UnitPrice=model.UnitPrice,
                };
                await db.OrderDetails.AddAsync(obj);
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
