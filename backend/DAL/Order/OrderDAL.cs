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
                    State = model.State,
                    DeliveryAddress = model.DeliveryAddress,
                    DeliveryEmail = model.DeliveryEmail,
                    DeliveryPhone = model.DeliveryPhone,
                    CreatedAt = model.CreatedAt,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Note = model.Note,
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
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.State != 0 && x.State != 4).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>(),
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderVM>> GetOrderByUserIdState0(string userId)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.State == 0).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>(),
                    FirstName=x.FirstName,
                    LastName=x.FirstName,
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderVM>> GetOrderByUserIdState4(string userId)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.UserId == userId && x.State == 4).OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = null,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
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
                    resultFromDb = resultFromDb.Where(x => x.Id.ToLower() == query.ToLower()).ToList();
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
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = null,
                    FirstName=x.FirstName,
                    LastName=x.LastName
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
                    State = resultFromDb.State,
                    DeliveryAddress = resultFromDb.DeliveryAddress,
                    DeliveryEmail = resultFromDb.DeliveryEmail,
                    DeliveryPhone = resultFromDb.DeliveryPhone,
                    CreatedAt = resultFromDb.CreatedAt,
                    OrderDetailVMs = null,
                    FirstName=resultFromDb.FirstName,
                    LastName=resultFromDb.LastName,
                };
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeState(string id, int status)
        {
            try
            {
                var resultFromDb = await db.Orders.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.State = status;
                if (status == 4)
                {
                    resultFromDb.ReceivedAt = DateTime.Now;
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

        public async Task<List<OrderVM>> AdminGetByState(int? state)
        {
            try
            {
                var resultFromDb = await db.Orders
                    .OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (state.HasValue)
                {
                    resultFromDb = await db.Orders.Where(x => x.State == state)
                    .OrderByDescending(x => x.CreatedAt).ToListAsync();
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var order = resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = new List<OrderDetailVM>(),
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                }).ToList();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public async Task<OrderVM> GetFullById(string id)
        {
            try
            {
                var resultFromDb = await db.Orders.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var order = new OrderVM
                {
                    Id = resultFromDb.Id,
                    UserId = resultFromDb.UserId,
                    Amount = resultFromDb.Amount,
                    State = resultFromDb.State,
                    DeliveryAddress = resultFromDb.DeliveryAddress,
                    DeliveryEmail = resultFromDb.DeliveryEmail,
                    DeliveryPhone = resultFromDb.DeliveryPhone,
                    CreatedAt = resultFromDb.CreatedAt,
                    FirstName = resultFromDb.FirstName,
                    LastName = resultFromDb.LastName,
                    Note = resultFromDb.Note,
                    OrderDetailVMs = new List<OrderDetailVM>(),
                    
                };
                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<OrderVM>> OrderToday(int? state)
        {
            try
            {
                var resultFromDb = await db.Orders
                    .Where(x => x.CreatedAt.Year == DateTime.Today.Year
                        && x.CreatedAt.Month == DateTime.Today.Month
                        && x.CreatedAt.Day == DateTime.Today.Day)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                if (state.HasValue)
                {
                    resultFromDb = resultFromDb.Where(x => x.State == state).ToList();
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
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Note = x.Note,
                    OrderDetailVMs = new List<OrderDetailVM>()
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<List<OrderVM>> OrderChart(int year)
        {
            try
            {
                var resultFromDb = await db.Orders.Where(x => x.CreatedAt.Year == year && x.State==4).ToListAsync();
                return resultFromDb.Select(x => new OrderVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    State = x.State,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryEmail = x.DeliveryEmail,
                    DeliveryPhone = x.DeliveryPhone,
                    CreatedAt = x.CreatedAt,
                    OrderDetailVMs = null,
                    FirstName = x.FirstName,
                    LastName = x.FirstName,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }
    
        
    }
}
