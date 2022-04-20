using BO;
using BO.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Order
{
    public class OrderDAL
    {
        private AppDbContext db;
        public OrderDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> Create(OrderVM model)
        {
            try
            {
                var obj = new BO.Entities.Order
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Amount = model.Amount,
                    Status = model.Status,
                    Discount = model.Discount,
                    DeliveryAddress = model.DeliveryAddress,
                    DeliveryEmail=model.DeliveryEmail,
                    DeliveryPhone=model.DeliveryPhone,
                    CreatedAt=model.CreatedAt,
                };
                await db.Orders.AddAsync(obj);
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
