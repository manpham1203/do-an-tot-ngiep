using BLL.OrderDetail;
using BLL.Product;
using BO.ViewModels.Order;
using BO.ViewModels.OrderDetail;
using DAL.Order;
using MailKit.Net.Smtp;
using MimeKit;
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
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Note = model.Note,
                    Amount = amount,
                    CreatedAt = DateTime.Now,
                    State = 1,
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
                    var detail = await detailBLL.GetListDetailByOrderId(resultFromDAL[i].Id);
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
        public async Task<List<OrderVM>> GetOrderByUserIdState0(string userId)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetOrderByUserIdState0(userId);
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
                    var detail = await detailBLL.GetListDetailByOrderId(resultFromDAL[i].Id);
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
        public async Task<List<OrderVM>> GetOrderByUserIdState4(string userId)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetOrderByUserIdState4(userId);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new List<OrderVM>();
                }
                //var detailBLL = new OrderDetailBLL();
                //for (int i = 0; i < resultFromDAL.Count; i++)
                //{
                //    var detail = await detailBLL.GetDetailByOrderId(resultFromDAL[i].Id);
                //    if (detail != null)
                //    {
                //        resultFromDAL[i].OrderDetailVMs = detail;
                //    }
                //}

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
                var resultFromDAL = await orderDAL.GetOrderByUserIdState4(userId);
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

                List<OrderDetailVM> resultList = tempList.GroupBy(x => x.ProductId).Select(x => x.First()).ToList();

                return resultList;
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

        public async Task<bool> ChangeState(string id, int State)
        {
            try
            {
                var checkId = await GetbyId(id);

                if (checkId == null)
                {
                    return false;
                }
                var result = await orderDAL.ChangeState(id, State);
                if (result == false)
                {
                    return false;
                }
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Admin", "datn.test1@gmail.com"));
                message.To.Add(new MailboxAddress("Client", checkId.DeliveryEmail));
                message.Subject = "Trạng thái đơn hàng thay đổi";
                //message.Body = new TextPart("plain")
                //{
                //    Text="hello",                    
                //};
                message.Body = new TextPart();
                switch (State)
                {
                    case 0:
                        message.Body = new TextPart()
                        {
                            Text = "Đơn hàng của bạn đã bị huỷ",
                        };
                        break;
                    case 1:
                        message.Body = new TextPart()
                        {
                            Text = "Đơn hàng của bạn đang chờ xác nhận",
                        };
                        break;
                    case 2:
                        message.Body = new TextPart()
                        {
                            Text = "Đơn hàng của bạn đã được xác nhận, đang chuẩn bị hàng",
                        };
                        break;
                    case 3:
                        message.Body = new TextPart()
                        {
                            Text = "Đơn hàng của bạn đã giao cho đơn vị vận chuyển",
                        };
                        break;
                    case 4:
                        message.Body = new TextPart()
                        {
                            Text = "Đơn hàng của bạn đã giao thành công",
                        };
                        break;
                    default:
                        break;
                }
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("datn.test1@gmail.com", "phamminhman");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public async Task<OrderPaginationVM> AdminGetByState(int currentPage, int limit, string id, int? state)
        {
            try
            {
                var resultFromDAL = await orderDAL.AdminGetByState(state);

                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new OrderPaginationVM();
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
                if (!string.IsNullOrEmpty(id))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Id.ToLower() == id.ToLower()).ToList();
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
        public async Task<OrderVM> GetFullById(string id)
        {
            try
            {
                var resultFromDAL = await orderDAL.GetFullById(id);
                if (resultFromDAL == null)
                {
                    return null;
                }
                var detailBLL = new OrderDetailBLL();
                var detail = await detailBLL.GetListDetailByOrderId(resultFromDAL.Id);
                if (detail != null)
                {
                    resultFromDAL.OrderDetailVMs = detail;
                }
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }

        public async Task<OrderPaginationVM> OrderToday(int? state, int currentPage, int limit)
        {
            try
            {
                var resultFromDAL = await orderDAL.OrderToday(state);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new OrderPaginationVM
                    {
                        TotalResult = 0,
                        TotalPage = 0,
                        OrderVMs = new List<OrderVM>(),
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

        public async Task<List<OrderChartVM>> OrderChart()
        {
            try
            {
                var resultFromDAL = await orderDAL.OrderChart();
                var chart = new List<OrderChartVM>();

                for (int i = 1; i <= 12; i++)
                {
                    var t1 = resultFromDAL.Where(x => x.CreatedAt.Month == i).ToList();
                    //var now = t1.Where(x => x.CreatedAt.Year == DateTime.Today.Year
                    //&& x.CreatedAt.Month==DateTime.Today.Month).ToList();
                    
                    if (t1.Count == 0)
                    {

                        chart.Add(new OrderChartVM
                        {
                            Id = i,
                            Name = "Tháng " + i,
                            Value = null
                        });
                    }
                    else
                    {
                        //var now = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                        //var check = t1.Where(x => x.CreatedAt.Month > now.Month && x.CreatedAt.Year >= now.Year).ToList();
                        //if (check.Count == 0)
                        //{
                        //    continue;
                        //}
                        chart.Add(new OrderChartVM
                        {
                            Id = i,
                            Name = "Tháng " + i,
                            Value = t1.Sum(x => x.Amount)
                        });
                    }

                }
                return chart;
            }
            catch
            {
                return null;
            }
        }
    }
}
