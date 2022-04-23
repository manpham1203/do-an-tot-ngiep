using BO;
using BO.ViewModels.Order;
using BO.ViewModels.OrderDetail;
using Microsoft.EntityFrameworkCore;
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
                    DeliveryEmail = model.DeliveryEmail,
                    DeliveryPhone = model.DeliveryPhone,
                    CreatedAt = model.CreatedAt,
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
        public async Task<List<OrderVM>> GetOrderByUserId(string userId)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.Status != 0 && x.Status!=4).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    Status = x.Status,
                    Discount = x.Discount,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>()
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderVM>> GetOrderByUserIdStatus0(string userId)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.Status == 0).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    Status = x.Status,
                    Discount = x.Discount,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>()
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderVM>> GetOrderByUserIdStatus4(string userId)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.Status == 4).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    Status = x.Status,
                    Discount = x.Discount,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>()
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<OrderVM>> GetAll(string query)
        {
            try
            {
                var resultFromDb = await db.Orders.OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (!string.IsNullOrEmpty(query))
                {
                    resultFromDb=resultFromDb.Where(x => x.Id.ToLower() == query.ToLower()).ToList();
                }
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var result = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    Status = x.Status,
                    Discount = x.Discount,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = null
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<OrderVM> GetById(string id)
        {
            try
            {
                var resultFromDb = await db.Orders.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new OrderVM
                {
                    Id = resultFromDb.Id,
                    UserId = resultFromDb.UserId,
                    Amount = resultFromDb.Amount,
                    Status = resultFromDb.Status,
                    Discount = resultFromDb.Discount,
                    DeliveryAddress = resultFromDb.DeliveryAddress,
                    DeliveryEmail = resultFromDb.DeliveryEmail,
                    DeliveryPhone = resultFromDb.DeliveryPhone,
                    CreatedAt = resultFromDb.CreatedAt,
                    OrderDetailVMs = null
                };
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeStatus(string id, int status)
        {
            try
            {
                var resultFromDb = await db.Orders.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.Status = status;
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
