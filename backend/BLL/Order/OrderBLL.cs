using BLL.OrderDetail;
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
        public OrderBLL()
        {
            orderDAL = new OrderDAL();
        }
        public async Task<bool> Create(List<OrderDetailVM> model)
        {
            try
            {
                var detailBLL = new OrderDetailBLL();
                var resultCreateDetail = detailBLL.Create(model);
                return await orderDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
    }
}
