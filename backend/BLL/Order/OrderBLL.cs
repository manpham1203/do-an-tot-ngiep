using BLL.OrderDetail;
using BLL.Product;
using BO.ViewModels.Order;
using BO.ViewModels.OrderDetail;
using DAL.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Order
{
    public class OrderBLL
    {
        private OrderDAL orderDAL;
        private CommonBLL cm;
        public OrderBLL()
        {
            orderDAL = new OrderDAL();
        }
        public async Task<bool> Create(CheckoutVM model)
        {
            try
            {
                cm = new CommonBLL();
                var detailBLL = new OrderDetailBLL();

                var orderId = cm.RandomString(12);

                var amount = model.OrderDetailVMs.Sum(x => x.UnitPrice * x.Quantity);
                var orderObj = new OrderVM
                {
                    Id = orderId,
                    UserId = model.UserId,
                    DeliveryAddress = model.DeliveryAddress,
                    DeliveryEmail = model.DeliveryEmail,
                    DeliveryPhone = model.DeliveryPhone,
                    Amount = amount,
                    CreatedAt = DateTime.Now,
                    Status=1,
                };
                var resulrOrder = await orderDAL.Create(orderObj);
                if (resulrOrder == false)
                {
                    return false;
                }
                var detailObj = model.OrderDetailVMs;

                return await detailBLL.Create(detailObj, orderId);
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
                var resultFromDAL = await orderDAL.GetOrderByUserId(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var detailBLL = new OrderDetailBLL();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    var detail = await detailBLL.GetDetailByOrderId(resultFromDAL[i].Id);
                    if (detail != null)
                    {
                        resultFromDAL[i].OrderDetailVMs = detail;
                    }
                }

                return resultFromDAL;
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
                var resultFromDAL = await orderDAL.GetOrderByUserIdStatus0(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var detailBLL = new OrderDetailBLL();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    var detail = await detailBLL.GetDetailByOrderId(resultFromDAL[i].Id);
                    if (detail != null)
                    {
                        resultFromDAL[i].OrderDetailVMs = detail;
                    }
                }

                return resultFromDAL;
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
                var resultFromDAL = await orderDAL.GetOrderByUserIdStatus4(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<OrderVM>();
                }
                var detailBLL = new OrderDetailBLL();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    var detail = await detailBLL.GetDetailByOrderId(resultFromDAL[i].Id);
                    if (detail != null)
                    {
                        resultFromDAL[i].OrderDetailVMs = detail;
                    }
                }

                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderDetailVM>> PurchasedProduct(string userId)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetOrderByUserIdStatus4(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<OrderDetailVM>();
                }
                var detailBLL = new OrderDetailBLL();
                var tempList = new List<OrderDetailVM>();
                for (int i = 0; i < resultFromDAL.Count; i++)
                {
                    var detail = await detailBLL.OrderedProduct(resultFromDAL[i].Id);
                    tempList.AddRange(detail);
                }
                for(int i = 0; i < tempList.Count; i++)
                {
                    for(int j = 0; j < tempList.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        if(tempList[i].ProductId == tempList[j].ProductId)
                        {
                            tempList.Remove(tempList[j]);
                        }
                    }
                }

                return tempList;
            }
            catch
            {
                return null;
            }
        }
        public async Task<OrderPaginationVM> GetAll(int limit, int currentPage, string query)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetAll(query);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new OrderPaginationVM
                    {
                        OrderVMs = new List<OrderVM>(),
                        TotalPage = 0,
                        TotalResult = 0,
                    };
                }

                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();


                return new OrderPaginationVM
                {
                    OrderVMs = resultFromDAL,
                    TotalPage = totalPage,
                    TotalResult = count,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<OrderVM> GetbyId(string id)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetById(id);
                if (resultFromDAL == null)
                {
                    return null;
                }
                return resultFromDAL;
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
                var checkId = await GetbyId(id);
                if (checkId == null)
                {
                    return false;
                }
                var result = await orderDAL.ChangeStatus(id, status);
                if (result == false)
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
    }
}
