using BO;
using BO.ViewModels.OrderDetail;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> Create(List<OrderDetailVM> model)
        {
            try
            {
                var obj = model.Select(x=>new BO.Entities.OrderDetail
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId=x.ProductId,
                    Quantity=x.Quantity,
                    UnitPrice=x.UnitPrice,
                    CreatedAt=x.CreatedAt,
                });
                await db.OrderDetails.AddRangeAsync(obj);
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
    
        public async Task<List<OrderDetailVM>> GetDetailByOrderId(string orderId)
        {
            try
            {
                var resultFromDb = await db.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if(resultFromDb.Count == 0)
                {
                    return new List<OrderDetailVM>();
                }
                var result = resultFromDb.Select(x => new OrderDetailVM { 
                    Id=x.Id,
                    OrderId=x.OrderId,
                    ProductId=x.ProductId,
                    Quantity=x.Quantity,
                    UnitPrice=x.UnitPrice,
                    ProductOrderVM=null,
                    CartRowVM=null
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    
    }
}
